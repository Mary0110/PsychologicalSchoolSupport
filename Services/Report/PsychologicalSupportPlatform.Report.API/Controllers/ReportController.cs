using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PsychologicalSupportPlatform.Common;
using PsychologicalSupportPlatform.Report.API.Extensions;
using PsychologicalSupportPlatform.Report.Application.DTOs;
using PsychologicalSupportPlatform.Report.Application.Interfaces;

namespace PsychologicalSupportPlatform.Report.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _service;

        public ReportController(IReportService formService)
        {
            _service = formService;
        }
        
        [HttpPost]
        [Authorize(Roles = Roles.Psychologist)]
        public async Task<IActionResult> GenerateReportAsync(GenerateReportInfoDTO infoDto, CancellationToken token)
        {
            var userId = User.GetLoggedInUserId();
            var dto = new GenerateReportDTO(infoDto.meetId, infoDto.comments, infoDto.conclusion, 
                int.Parse(userId));
            var memoryStream = await _service.GenerateReportAsync(dto, token);
        
            return File(memoryStream, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", $"report{infoDto.meetId}.docx");
        }
    }
}

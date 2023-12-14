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
    public class ReportsController : ControllerBase
    { 
        private readonly IReportService _reportService;
        private readonly IMonthlyReportService _monthlyReportService;

        public ReportsController(IReportService reportService, IMonthlyReportService monthlyReportService)
        {
            _reportService = reportService;
            _monthlyReportService = monthlyReportService;
        }
        
        [HttpPost]
        [Authorize(Roles = Roles.Psychologist)]
        public async Task<IActionResult> GenerateReportAsync(GenerateReportInfoDTO infoDto, CancellationToken token)
        {
            var userId = User.GetLoggedInUserId();
            var dto = new GenerateReportDTO(infoDto.meetId, infoDto.comments, infoDto.conclusion, 
                int.Parse(userId));
            var memoryStream = await _reportService.GenerateReportAsync(dto, token);
        
            return File(
                memoryStream, 
                "application/vnd.openxmlformats-officedocument.wordprocessingml.document", 
                $"report{infoDto.meetId}.docx"
                );
        }

        [HttpGet]
        [Authorize(Roles = Roles.Manager + "," + Roles.Psychologist)]
        public async Task<IActionResult> GetMonthlyReportAsync([FromQuery] GetMonthlyReportDTO num, CancellationToken token)
        {
            var memoryStream = await _monthlyReportService.GetMonthlyReportAsync(num, token);

            return File(
                memoryStream, 
                "application/vnd.openxmlformats-officedocument.wordprocessingml.document", 
                $"report{num.Month}.docx"
            );
        }
    }
}

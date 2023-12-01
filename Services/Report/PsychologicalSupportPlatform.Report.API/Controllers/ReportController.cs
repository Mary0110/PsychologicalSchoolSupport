using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        // [Authorize(Roles = Roles.Psychologist)]
        public async Task<IActionResult> GenerateReportAsync(GenerateReportDTO dto)
        {
            var userId = User.GetLoggedInUserId();
            await _service.GenerateReportAsync(dto.meetId, dto.comments, userId);

            return Ok();
        }
    }
}

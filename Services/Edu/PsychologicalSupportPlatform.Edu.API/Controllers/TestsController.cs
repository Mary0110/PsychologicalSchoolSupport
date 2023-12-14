using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PsychologicalSupportPlatform.Common;
using PsychologicalSupportPlatform.Edu.API.Extensions;
using PsychologicalSupportPlatform.Edu.Application.DTOs.Tests;
using PsychologicalSupportPlatform.Edu.Application.Interfaces;
using PsychologicalSupportPlatform.Edu.Application.Interfaces.Tests;

namespace PsychologicalSupportPlatform.Edu.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestsController : ControllerBase
    {
        private readonly ITestService _psychologicalTestService;

        public TestsController(ITestService psychologicalTestService)
        {
            _psychologicalTestService = psychologicalTestService;
        }
        
        [HttpPost("{testId}/pass")]
        [Authorize]
        public async Task<IActionResult> PassTest([FromRoute] int testId, [FromBody] List<AnswerRequestDTO> answers)
        {
            var userId = User.GetLoggedInUserId();
            var passDTO = new PassTestDTO(){TestId = testId, UserId = userId, Answers = answers};
            await _psychologicalTestService.PassTestAsync(passDTO);

            return Ok();
        }
        
        [HttpGet("{userId}")]
        [Authorize(Roles = Roles.Psychologist)]
        public async Task<IActionResult> GetResults([FromRoute] int userId, [FromBody] int pageNumber, [FromBody] int pageSize, CancellationToken token)
        {
            var results = await _psychologicalTestService.GetTestResultsByStudentAsync(userId, pageNumber, pageSize, token);

            return Ok(results);
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PsychologicalSupportPlatform.Common;
using PsychologicalSupportPlatform.Edu.API.Extensions;
using PsychologicalSupportPlatform.Edu.Application.DTOs.Tests;
using PsychologicalSupportPlatform.Edu.Application.Interfaces.Tests.Services;

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
        
        [HttpPost("evaluation")]
        [Authorize]
        public async Task<IActionResult> PassTest([FromBody] AnswerRequestDTO answers)
        {
            var userId = User.GetLoggedInUserId();
            var passDTO = new UserAnswerRequestDTO(){UserId = userId, AnswerRequestDTO = answers};
            await _psychologicalTestService.PassTestAsync(passDTO);

            return Ok();
        }
        
        [HttpGet("{userId}")]
        [Authorize(Roles = Roles.Psychologist)]
        public async Task<IActionResult> GetResults([FromRoute] int userId, [FromQuery]  int pageNumber, [FromQuery] int pageSize, CancellationToken token)
        {
            var results = await _psychologicalTestService.GetTestResultsByStudentAsync(userId, pageNumber, pageSize, token);

            return Ok(results);
        }
        
        [HttpPost]
        [Authorize(Roles = Roles.Admin + "," + Roles.Psychologist)]
        public async Task<IActionResult> AddTestAsync(AddTetsDTO addProductDto)
        {
            var response = await _psychologicalTestService.AddTestAsync(addProductDto);

            return Ok(response);
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PsychologicalSupportPlatform.Authorization.Application.Interfaces;
using PsychologicalSupportPlatform.Authorization.Domain.DTOs;
using PsychologicalSupportPlatform.Common;

namespace PsychologicalSupportPlatform.Authorization.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormsController : ControllerBase
    {
        private readonly IFormService _formService;

        public FormsController(IFormService formService)
        {
            _formService = formService;
        }
        
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllFormsAsync([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var response = await _formService.GetAllFormsAsync(pageNumber, pageSize);
            
            return response.ToActionResult();
        }

        [HttpPost("{num}/{letter}")]
        [Authorize(Roles = Roles.Admin + "," + Roles.Manager)]
        public async Task<IActionResult> AddFormAsync([FromRoute] int num, [FromRoute] char letter)
        {
            var response = await _formService.AddFormAsync(num, letter);

            return response.ToActionResult();
        }

        [HttpDelete("{num}/{letter}")]
        [Authorize(Roles = Roles.Admin + "," + Roles.Manager)]
        public async Task<IActionResult> RemoveFormAsync([FromRoute] int num, [FromRoute] char letter)
        {
            var response = await _formService.DeleteFormAsync(num, letter);
            
            return response.ToActionResult();
        }

        [HttpGet("parallel/{num}")]
        [Authorize]
        public async Task<IActionResult> GetFormsByParallelAsync([FromRoute] int num, [FromQuery] int pageNumber, [FromQuery] int pageSize)
        { 
            var response = await _formService.GetFormsByParallelAsync(num, pageNumber, pageSize);
        
            return response.ToActionResult();
        }
    }
}

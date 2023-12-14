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
        private readonly IFormService formService;

        public FormsController(IFormService formService)
        {
            this.formService = formService;
        }
        
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllFormsAsync([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var response = await formService.GetAllFormsAsync(pageNumber, pageSize);
            
            return Ok(response.Data);
        }

        [HttpPost("{num}/{letter}")]
        [Authorize(Roles = Roles.Admin + "," + Roles.Manager)]
        public async Task<IActionResult> AddFormAsync([FromRoute] int num, [FromRoute] char letter)
        {
            var response = await formService.AddFormAsync(num, letter);

            return Ok(response.Message);
        }

        [HttpDelete("{num}/{letter}")]
        [Authorize(Roles = Roles.Admin + "," + Roles.Manager)]
        public async Task<IActionResult> RemoveFormAsync(int formNum, char formLetter)
        {
            var response = await formService.DeleteFormAsync(formNum, formLetter);

            if (!response.Success)
            {
                return NotFound(response.Message);
            }

            return Ok(response.Message);
        }
        
        [HttpGet("parallel/{num}/forms")]
        [Authorize]
        public async Task<IActionResult> GetFormsByParallelAsync([FromRoute] int num, [FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var response = await formService.GetFormsByParallelAsync(num, pageNumber, pageSize);

            if (!response.Success)
            {
                return NotFound(response.Message);
            }

            return Ok(response.Data);
        }
    }
}

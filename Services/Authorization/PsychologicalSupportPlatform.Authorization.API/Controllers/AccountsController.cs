using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PsychologicalSupportPlatform.Authorization.Application.Interfaces;
using PsychologicalSupportPlatform.Authorization.Domain.DTOs;
using PsychologicalSupportPlatform.Common;

namespace PsychologicalSupportPlatform.Authorization.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountsService _accountsService;

        public AccountsController(IAccountsService accountsService)
        {
            _accountsService = accountsService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginData data)
        {
            var response = await _accountsService.GetTokenAsync(data);

            return response.ToActionResult();
        }
        
        [HttpPost]
        [Route("user/register")]
        public async Task<IActionResult> RegisterUserAsync([FromBody] AddUserDTO user)
        {
            var response = await _accountsService.RegisterUserAsync(user);
            
            return response.ToActionResult();
        }
        
        [HttpPost]
        [Route("student/register")]
        public async Task<IActionResult> RegisterStudentAsync([FromBody] AddStudentDTO student)
        {
            var response = await _accountsService.RegisterStudentAsync(student);
            
            return response.ToActionResult();
        }

        [HttpGet("users")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> GetAllUsersAsync(
            [FromQuery] int pageNumber, [FromQuery] int pageSize
            )
        {
            var response = await _accountsService.GetAllUsersAsync(pageNumber, pageSize);
            
            return response.ToActionResult();
        }
        
        [HttpGet("users/{id}")]
        [Authorize(Roles = Roles.Admin + "," + Roles.Psychologist + "," + Roles.Manager)]
        public async Task<IActionResult> GetSingleUserAsync(int id)
        {
            var response = await _accountsService.GetUserByIdAsync(id);

            return response.ToActionResult();
        }
        
        [HttpPut("users/{id}")]
        [Authorize(Roles = Roles.Admin + "," + Roles.Psychologist + "," + Roles.Manager)]
        public async Task<IActionResult> UpdateUserAsync(int id, [FromBody] UpdateUserDTO user)
        {
            var response = await _accountsService.UpdateUserAsync(id, user);

            return response.ToActionResult();
        }
        
        [HttpDelete("users/{id}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> DeleteUserAsync(int id)
        {
            var response = await _accountsService.DeleteUserAsync(id);
            
            return response.ToActionResult();
        }
        
        [HttpGet("students")]
        [Authorize(Roles = Roles.Admin + "," + Roles.Psychologist + "," + Roles.Manager)]
        public async Task<IActionResult> GetAllStudentsAsync(
            [FromQuery] int pageNumber, [FromQuery] int pageSize
            )
        {
            var response = await _accountsService.GetAllStudentsAsync(pageNumber, pageSize);
            
            return response.ToActionResult();
        }
        
        [HttpGet("students/{id}")]
        [Authorize(Roles = Roles.Admin + "," + Roles.Psychologist + "," + Roles.Manager)]
        public async Task<IActionResult> GetSingleStudentAsync(int id)
        {
            var response = await _accountsService.GetStudentByIdAsync(id);
            
            return response.ToActionResult();
        }
        
        [HttpGet("forms/{formNum}/{formLetter}/students")]
        [Authorize(Roles = Roles.Admin + "," + Roles.Psychologist + "," + Roles.Manager)]
        public async Task<IActionResult> GetStudentsByFormAsync(
            [FromRoute] int formNum, [FromRoute] char formLetter, 
            [FromQuery] int pageNumber, [FromQuery] int pageSize
            )
        {
            var formDto = new AddFormDTO(formNum, formLetter);
            var response = await _accountsService.GetStudentsByFormAsync(formDto, pageNumber, pageSize);
            
            return response.ToActionResult();
        }
        
        [HttpGet("parallel/{num}/students")]
        [Authorize(Roles = Roles.Admin + "," + Roles.Psychologist + "," + Roles.Manager)]
        public async Task<IActionResult> GetStudentsByParallelAsync(int num, 
            [FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var response = await _accountsService.GetStudentsByParallelAsync(num, pageNumber, pageSize);
            
            return response.ToActionResult();
        }
        
        [HttpPut("students/{id}")]
        [Authorize(Roles = Roles.Admin + "," + Roles.Psychologist + "," + Roles.Manager)]
        public async Task<IActionResult> UpdateStudentAsync(int id, [FromBody] UpdateStudentDTO user)
        {
            var response = await _accountsService.UpdateStudentAsync(id, user);

            return response.ToActionResult();
        }

        [HttpDelete("students/{id}")]
        [Authorize(Roles = Roles.Admin + "," + Roles.Psychologist + "," + Roles.Manager)]
        public async Task<IActionResult> DeleteStudentAsync(int id)
        {
            var response = await _accountsService.DeleteStudentAsync(id);

            return response.ToActionResult();
        }
    }
}

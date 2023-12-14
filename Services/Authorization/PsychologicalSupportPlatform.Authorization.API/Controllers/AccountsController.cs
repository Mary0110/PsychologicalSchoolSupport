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
        public async Task<IActionResult> LoginAsync(LoginData data)
        {
            var response = await _accountsService.GetTokenAsync(data);

            if (response.Data is null)
            {
                return Unauthorized(response.Message);
            }

            return Ok(new { access_token = response.Data });
        }
        
        [HttpPost]
        [Route("user/register")]
        public async Task<IActionResult> RegisterUserAsync(AddUserDTO user)
        {
            var response = await _accountsService.RegisterUserAsync(user);

            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            return Ok(response.Message);
        }
        
        [HttpPost]
        [Route("student/register")]
        public async Task<IActionResult> RegisterStudentAsync(AddStudentDTO student)
        {
            var response = await _accountsService.RegisterStudentAsync(student);

            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            return Ok(response.Message);
        }

        [HttpGet("users")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> GetAllUsersAsync(
            [FromQuery] int pageNumber, [FromQuery] int pageSize
            )
        {
            var response = await _accountsService.GetAllUsersAsync(pageNumber, pageSize);
            
            return Ok(response.Data);
        }
        
        [HttpGet("users/{id}")]
        [Authorize(Roles = Roles.Admin + "," + Roles.Psychologist + "," + Roles.Manager)]
        public async Task<IActionResult> GetSingleUserAsync(int id)
        {
            var response = await _accountsService.GetUserByIdAsync(id);

            if (!response.Success)
            {
                return NotFound(response.Message);
            }

            return Ok(response.Data);
        }
        
        [HttpPut("users/{id}")]
        [Authorize(Roles = Roles.Admin + "," + Roles.Psychologist + "," + Roles.Manager)]
        public async Task<IActionResult> UpdateUserAsync(int id, UpdateUserDTO user)
        {
            
            var response = await _accountsService.UpdateUserAsync(id, user);

            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            return Ok(response.Message);
        }
        
        [HttpDelete("users/{id}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> DeleteUserAsync(int id)
        {
            var response = await _accountsService.DeleteUserAsync(id);

            if (!response.Success)
            {
                return NotFound(response.Message);
            }

            return Ok(response.Message);
        }
        
        [HttpGet("students")]
        [Authorize(Roles = Roles.Admin + "," + Roles.Psychologist + "," + Roles.Manager)]
        public async Task<IActionResult> GetAllStudentsAsync(
            [FromQuery] int pageNumber, [FromQuery] int pageSize
            )
        {
            var response = await _accountsService.GetAllStudentsAsync(pageNumber, pageSize);
            
            return Ok(response.Data);
        }
        
        [HttpGet("students/{id}")]
        [Authorize(Roles = Roles.Admin + "," + Roles.Psychologist + "," + Roles.Manager)]
        public async Task<IActionResult> GetSingleStudentAsync(int id)
        {
            var response = await _accountsService.GetStudentByIdAsync(id);

            if (!response.Success)
            {
                return NotFound(response.Message);
            }

            return Ok(response.Data);
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

            if (!response.Success)
            {
                return NotFound(response.Message);
            }

            return Ok(response.Data);
        }
        
        [HttpGet("parallel/{num}/students")]
        [Authorize(Roles = Roles.Admin + "," + Roles.Psychologist + "," + Roles.Manager)]
        public async Task<IActionResult> GetStudentsByParallelAsync(int num, 
            [FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var response = await _accountsService.GetStudentsByParallelAsync(num, pageNumber, pageSize);

            if (!response.Success)
            {
                return NotFound(response.Message);
            }

            return Ok(response.Data);
        }
        
        [HttpPut("students/{id}")]
        [Authorize(Roles = Roles.Admin + "," + Roles.Psychologist + "," + Roles.Manager)]
        public async Task<IActionResult> UpdateStudentAsync(int id, UpdateStudentDTO user)
        {
            var response = await _accountsService.UpdateStudentAsync(id, user);

            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            return Ok(response.Message);
        }

        [HttpDelete("students/{id}")]
        [Authorize(Roles = Roles.Admin + "," + Roles.Psychologist + "," + Roles.Manager)]
        public async Task<IActionResult> DeleteStudentAsync(int id)
        {
            var response = await _accountsService.DeleteStudentAsync(id);

            if (!response.Success)
            {
                return NotFound(response.Message);
            }

            return Ok(response.Message);
        }
    }
}

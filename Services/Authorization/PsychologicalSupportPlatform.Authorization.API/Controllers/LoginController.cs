using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PsychologicalSupportPlatform.Authorization.Application.Interfaces;
using PsychologicalSupportPlatform.Authorization.Domain.DTOs;
using PsychologicalSupportPlatform.Authorization.Domain.Entities;
using PsychologicalSupportPlatform.Common;

namespace PsychologicalSupportPlatform.Authorization.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService loginService;

        public LoginController(ILoginService loginService)
        {
            this.loginService = loginService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginAsync(LoginData data)
        {
            var response = await loginService.GetTokenAsync(data);

            if (response.Data is null) return Unauthorized(response.Message);

            return Ok(new { access_token = response.Data });
        }
        
        [HttpPost]
        [Route("register/user")]
        public async Task<IActionResult> RegisterUserAsync(AddUserDTO user)
        {
            var response = await loginService.RegisterUserAsync(user);

            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            return Ok(response.Message);
        }
        
        [HttpPost]
        [Route("register/student")]
        public async Task<IActionResult> RegisterStudentAsync(AddStudentDTO student)
        {
            var response = await loginService.RegisterStudentAsync(student);

            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            return Ok(response.Message);
        }

        [HttpGet("users")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> GetAllUsersAsync([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var response = await loginService.GetAllUsersAsync(pageNumber, pageSize);
            
            return Ok(response.Data);
        }
        
        [HttpGet("user/{id}")]
        [Authorize(Roles = Roles.Admin + "," + Roles.Psychologist + "," + Roles.Manager)]
        public async Task<IActionResult> GetSingleUserAsync(int id)
        {
            var response = await loginService.GetUserByIdAsync(id);

            if (!response.Success)
            {
                return NotFound(response.Message);
            }

            return Ok(response.Data);
        }
        
        [HttpPut("user")]
        [Authorize(Roles = Roles.Admin + "," + Roles.Psychologist + "," + Roles.Manager)]
        public async Task<IActionResult> UpdateUserAsync(UpdateUserDTO user)
        {
            var response = await loginService.UpdateUserAsync(user);

            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            return Ok(response.Message);
        }
        
        [HttpDelete("user/{id}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> DeleteUserAsync(int id)
        {
            var response = await loginService.DeleteUserAsync(id);

            if (!response.Success)
            {
                return NotFound(response.Message);
            }

            return Ok(response.Message);
        }
        
        [HttpGet("students")]
        [Authorize(Roles = Roles.Admin + "," + Roles.Psychologist + "," + Roles.Manager)]
        public async Task<IActionResult> GetAllStudentsAsync([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var response = await loginService.GetAllStudentsAsync(pageNumber, pageSize);
            
            return Ok(response.Data);
        }
        
        [HttpGet("student/{id}")]
        [Authorize(Roles = Roles.Admin + "," + Roles.Psychologist + "," + Roles.Manager)]
        public async Task<IActionResult> GetSingleStudentAsync(int id)
        {
            var response = await loginService.GetStudentByIdAsync(id);

            if (!response.Success)
            {
                return NotFound(response.Message);
            }

            return Ok(response.Data);
        }
        
        [HttpGet("students/byForm/{num}/{letter}")]
        [Authorize(Roles = Roles.Admin + "," + Roles.Psychologist + "," + Roles.Manager)]
        public async Task<IActionResult> GetStudentsByFormAsync(int num, char letter, [FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var formDto = new AddFormDTO(num, letter);
            var response = await loginService.GetStudentsByFormAsync(formDto, pageNumber, pageSize);

            if (!response.Success)
            {
                return NotFound(response.Message);
            }

            return Ok(response.Data);
        }
        
        [HttpGet("students/byParallel/{num}")]
        [Authorize(Roles = Roles.Admin + "," + Roles.Psychologist + "," + Roles.Manager)]
        public async Task<IActionResult> GetStudentsByParallelAsync(int num, [FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var response = await loginService.GetStudentsByParallelAsync(num, pageNumber, pageSize);

            if (!response.Success)
            {
                return NotFound(response.Message);
            }

            return Ok(response.Data);
        }
        
        [HttpPut("student")]
        [Authorize(Roles = Roles.Admin + "," + Roles.Psychologist + "," + Roles.Manager)]
        public async Task<IActionResult> UpdateStudentAsync(UpdateStudentDTO user)
        {
            var response = await loginService.UpdateStudentAsync(user);

            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            return Ok(response.Message);
        }

        [HttpDelete("student/{id}")]
        [Authorize(Roles = Roles.Admin + "," + Roles.Psychologist + "," + Roles.Manager)]
        public async Task<IActionResult> DeleteStudentAsync(int id)
        {
            var response = await loginService.DeleteStudentAsync(id);

            if (!response.Success)
            {
                return NotFound(response.Message);
            }

            return Ok(response.Message);
        }
    }
}

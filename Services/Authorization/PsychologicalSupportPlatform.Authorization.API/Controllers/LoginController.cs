using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PsychologicalSupportPlatform.Authorization.Application.Interfaces;
using PsychologicalSupportPlatform.Authorization.Domain.DTOs;

namespace PsychologicalSupportPlatform.Authorization.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService loginService;
        private readonly IEncryptionService encryption;

        public LoginController(ILoginService loginService, IEncryptionService encryption)
        {
            this.loginService = loginService;
            this.encryption = encryption;
        }

        [HttpPost]
        [Route("/Login")]
        public async Task<IActionResult> LoginAsync(LoginData data)
        {
            data.Password = encryption.HashPassword(data.Password);
            var response = await loginService.GetTokenAsync(data);

            if (response.Data is null) return Unauthorized(response.Message);

            return Ok(new { access_token = response.Data });
        }
        
        [HttpPost]
        [Route("/Register/user")]
        public async Task<IActionResult> RegisterUserAsync(AddUserDTO user)
        {
            user.Password = encryption.HashPassword(user.Password);

            var response = await loginService.RegisterUserAsync(user);

            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            return Ok(response.Message);
        }
        
        [HttpPost]
        [Route("/Register/student")]
        public async Task<IActionResult> RegisterStudentAsync(AddStudentDTO student)
        {
            student.Password = encryption.HashPassword(student.Password);

            var response = await loginService.RegisterStudentAsync(student);

            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            return Ok(response.Message);
        }

        [HttpGet("users")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            var response = await loginService.GetAllUsersAsync();
            
            return Ok(response.Data);
        }
        
        [HttpGet("user/{id}")]
        [Authorize(Roles = "Admin, Psychologist, Manager")]
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
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> UpdateUserAsync(UpdateUserDTO user)
        {
            user.Password = encryption.HashPassword(user.Password);

            var response = await loginService.UpdateUserAsync(user);

            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            return Ok(response.Message);
        }
        
        [HttpDelete("user/{id}")]
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin, Psychologist, Manager")]
        public async Task<IActionResult> GetAllStudentsAsync()
        {
            var response = await loginService.GetAllStudentsAsync();
            
            return Ok(response.Data);
        }
        
        [HttpGet("student/{id}")]
        [Authorize(Roles = "Admin, Psychologist, Manager")]
        public async Task<IActionResult> GetSingleStudentAsync(int id)
        {
            var response = await loginService.GetStudentByIdAsync(id);

            if (!response.Success)
            {
                return NotFound(response.Message);
            }

            return Ok(response.Data);
        }
        
        [HttpPut("student")]
        [Authorize(Roles = "Admin, Psychologist, Manager")]
        public async Task<IActionResult> UpdateStudentAsync(UpdateStudentDTO user)
        {
            user.Password = encryption.HashPassword(user.Password);

            var response = await loginService.UpdateStudentAsync(user);

            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            return Ok(response.Message);
        }

        [HttpDelete("student/{id}")]
        [Authorize(Roles = "Admin, Psychologist, Manager")]
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

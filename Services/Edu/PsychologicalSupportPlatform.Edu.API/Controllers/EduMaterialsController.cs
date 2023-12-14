using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PsychologicalSupportPlatform.Common;
using PsychologicalSupportPlatform.Edu.Application.DTOs;
using PsychologicalSupportPlatform.Edu.Application.Interfaces;

namespace PsychologicalSupportPlatform.Edu.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EduMaterialsController : ControllerBase
    {
        private readonly IEduMaterialService _reportService;

        public EduMaterialsController(IEduMaterialService reportService)
        {
            _reportService = reportService;
        }
        
        [HttpGet("{id]")]
        [Authorize]
        public async Task<IActionResult> DownloadEduMaterial(int id, CancellationToken token)
        {
            var memoryStream = await _reportService.DownloadEduMaterialAsync(id, token);
        
            return File(
                memoryStream, 
                "application/vnd.openxmlformats-officedocument.wordprocessingml.document", 
                $"doc{id}.docx"
            );
        }

        [HttpPost("upload")]
        [Authorize(Roles = Roles.Psychologist)]
        public async Task<IActionResult> UploadEduMaterial(IFormFile file, AddEduMaterialDTO dto)
        {
            var id = await _reportService.UploadEduMaterialAsync(file, dto);

            return Ok(id);
        }
        
        [HttpPost]
        [Authorize(Roles = Roles.Psychologist)]
        public async Task<IActionResult> AddMaterialToStudent(AddEduMaterialToStudentDTO dto, CancellationToken token)
        {
            await _reportService.AddEduMaterialToStudent(dto, token);

            return Ok();
        }
        
        [HttpGet("student={studentId}")]
        [Authorize(Roles = Roles.Psychologist +","+Roles.Student)]
        public async Task<IActionResult> GetMaterialsByStudent(int studentId, [FromQuery] int pageNumber, [FromQuery] int pageSize, CancellationToken token)
        {
            var materials = await _reportService.GetEduMaterialsByStudentAsync(studentId, pageNumber, pageSize, token);

            return Ok(materials);
        }
        
        [HttpGet("materials")]
        [Authorize(Roles = Roles.Psychologist)]
        public async Task<IActionResult> GetMaterials([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var materials = await _reportService.GetAllEduMaterialsAsync(pageNumber, pageSize);

            return Ok(materials);
        }
    }
}

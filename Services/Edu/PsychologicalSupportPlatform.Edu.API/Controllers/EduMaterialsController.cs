using Microsoft.AspNetCore.Authorization;
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
        
        [HttpGet("search")]
        public async Task<IActionResult> SearchAsync(
            [FromQuery] string text,
            CancellationToken cancellationToken = default)
        {
            return Ok(await _reportService.SearchAsync(text, cancellationToken));
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

        [HttpPost]
        [Authorize(Roles = Roles.Psychologist)]
        public async Task<IActionResult> UploadEduMaterial(IFormFile file, [FromBody] AddEduMaterialDTO dto)
        {
            var addedId = await _reportService.UploadEduMaterialAsync(file, dto);

            return Ok(addedId);
        }
        
        [HttpPost("students/{studentId}/edu-materials/{id}")]
        [Authorize(Roles = Roles.Psychologist)]
        public async Task<IActionResult> AddMaterialToStudent([FromRoute] int studentId, [FromRoute] int id, CancellationToken token)
        {
            var dto = new AddEduMaterialToStudentDTO(studentId, id);
            await _reportService.AddEduMaterialToStudentAsync(dto, token);

            return Ok();
        }
        
        [HttpGet("students/{studentId}/edu-materials")]
        [Authorize(Roles = Roles.Psychologist +","+Roles.Student)]
        public async Task<IActionResult> GetMaterialsByStudent(int studentId, [FromQuery] int pageNumber, [FromQuery] int pageSize, CancellationToken token)
        {
            var materials = await _reportService.GetEduMaterialsByStudentAsync(studentId, pageNumber, pageSize, token);

            return Ok(materials);
        }
        
        [HttpGet]
        [Authorize(Roles = Roles.Psychologist)]
        public async Task<IActionResult> GetMaterials([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var materials = await _reportService.GetAllEduMaterialsAsync(pageNumber, pageSize);

            return Ok(materials);
        }
    }
}

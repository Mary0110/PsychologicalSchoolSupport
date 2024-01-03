using Microsoft.AspNetCore.Http;

namespace PsychologicalSupportPlatform.Edu.Application.DTOs;

public record AddEduMaterialDTO(IFormFile file, string Name, string Theme);

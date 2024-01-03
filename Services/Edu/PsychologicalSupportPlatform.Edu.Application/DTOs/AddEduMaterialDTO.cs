using Microsoft.AspNetCore.Http;
using PsychologicalSupportPlatform.Edu.Domain.Entities;

namespace PsychologicalSupportPlatform.Edu.Application.DTOs;

public record AddEduMaterialDTO(IFormFile file, string Name, string Theme);

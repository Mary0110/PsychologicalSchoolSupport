using PsychologicalSupportPlatform.Common.Repository;
using PsychologicalSupportPlatform.Edu.Application.DTOs;
using PsychologicalSupportPlatform.Edu.Domain.Entities;

namespace PsychologicalSupportPlatform.Edu.Application.Interfaces;

public interface IStudentHasEduMaterialRepository : ISQLRepository<StudentHasEduMaterial>
{
    Task<List<EduMaterial>> GetEduMaterialsByStudentAsync(GetEduMaterialByStudentDTO dto);
}

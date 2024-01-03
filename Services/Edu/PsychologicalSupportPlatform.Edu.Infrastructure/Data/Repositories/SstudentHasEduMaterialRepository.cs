using PsychologicalSupportPlatform.Common;
using PsychologicalSupportPlatform.Common.Repository;
using PsychologicalSupportPlatform.Edu.Application.DTOs;
using PsychologicalSupportPlatform.Edu.Application.Interfaces;
using PsychologicalSupportPlatform.Edu.Domain.Entities;

namespace PsychologicalSupportPlatform.Edu.Infrastructure.Data.Repositories;

public class StudentHasEduMaterialRepository: SQLRepository<DataContext, StudentHasEduMaterial>,IStudentHasEduMaterialRepository
{
    public StudentHasEduMaterialRepository(DataContext dbContext) : base(dbContext)
    {
    }
    
    public async Task<List<EduMaterial>> GetEduMaterialsByStudentAsync(GetEduMaterialByStudentDTO dto)
    {
        var result = DbContext.StudentHasEduMaterials
            .Where(shm => shm.StudentId == dto.StudentId)
            .Select(shm => shm.EduMaterial)
            .ToPagedCollection(dto.PageNumber, dto.PageSize)
            .ToList();

        return result;    
    }
}

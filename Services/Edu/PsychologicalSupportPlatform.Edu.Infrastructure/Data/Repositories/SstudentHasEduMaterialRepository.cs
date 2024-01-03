using PsychologicalSupportPlatform.Common;
using PsychologicalSupportPlatform.Common.Repository;
using PsychologicalSupportPlatform.Edu.Application.Interfaces;
using PsychologicalSupportPlatform.Edu.Domain.Entities;

namespace PsychologicalSupportPlatform.Edu.Infrastructure.Data.Repositories;

public class StudentHasEduMaterialRepository: SQLRepository<DataContext, StudentHasEduMaterial>,IStudentHasEduMaterialRepository
{
    public StudentHasEduMaterialRepository(DataContext dbContext) : base(dbContext)
    {
    }
    
    public async Task<List<EduMaterial>> GetEduMaterialsByStudentAsync(int studentId, int pageNumber, int pageSize)
    {
        var result = DbContext.StudentHasEduMaterials
            .Where(shm => shm.StudentId == studentId)
            .Select(shm => shm.EduMaterial)
            .ToPagedCollection(pageNumber, pageSize)
            .ToList();

        return result;    
    }
}

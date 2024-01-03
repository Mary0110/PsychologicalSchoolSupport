using PsychologicalSupportPlatform.Common.Repository;
using PsychologicalSupportPlatform.Edu.Application.Interfaces;
using PsychologicalSupportPlatform.Edu.Domain.Entities;

namespace PsychologicalSupportPlatform.Edu.Infrastructure.Data.Repositories;

public class EduMaterialRepository: SQLRepository<DataContext, EduMaterial>, IEduMaterialRepository
{
    public EduMaterialRepository(DataContext context) : base(context)
    {
    }
}

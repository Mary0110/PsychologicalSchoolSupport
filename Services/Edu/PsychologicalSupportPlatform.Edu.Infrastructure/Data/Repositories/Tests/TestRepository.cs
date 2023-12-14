
using PsychologicalSupportPlatform.Common.Repository;
using PsychologicalSupportPlatform.Edu.Application.Interfaces.Tests;
using PsychologicalSupportPlatform.Edu.Domain.Entities.Tests;

namespace PsychologicalSupportPlatform.Edu.Infrastructure.Data.Repositories.Tests;

public class TestRepository: SQLRepository<DataContext, Test>, ITestRepository
{
    public TestRepository(DataContext context) : base(context)
    {
    }
}

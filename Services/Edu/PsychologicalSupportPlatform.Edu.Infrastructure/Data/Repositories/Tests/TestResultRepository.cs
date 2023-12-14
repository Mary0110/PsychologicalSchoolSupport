using PsychologicalSupportPlatform.Common.Repository;
using PsychologicalSupportPlatform.Edu.Application.Interfaces.Tests;
using PsychologicalSupportPlatform.Edu.Domain.Entities.Tests;

namespace PsychologicalSupportPlatform.Edu.Infrastructure.Data.Repositories.Tests;

public class TestResultRepository: SQLRepository<DataContext, TestResult>, ITestResultRepository
{
    public TestResultRepository(DataContext context) : base(context)
    {
    }
}

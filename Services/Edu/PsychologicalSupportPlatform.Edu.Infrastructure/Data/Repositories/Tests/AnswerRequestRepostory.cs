using PsychologicalSupportPlatform.Common.Repository;
using PsychologicalSupportPlatform.Edu.Application.Interfaces.Tests;
using PsychologicalSupportPlatform.Edu.Domain.Entities.Tests;

namespace PsychologicalSupportPlatform.Edu.Infrastructure.Data.Repositories.Tests;

public class AnswerRequestRepository: SQLRepository<DataContext, AnswerRequest>, IAnswerRequestRepository
{
    public AnswerRequestRepository(DataContext context) : base(context)
    {
    }
}

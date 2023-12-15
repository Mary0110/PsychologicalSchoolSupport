using PsychologicalSupportPlatform.Common.Repository;
using PsychologicalSupportPlatform.Edu.Application.Interfaces.Tests;
using PsychologicalSupportPlatform.Edu.Application.Interfaces.Tests.Repositories;
using PsychologicalSupportPlatform.Edu.Domain.Entities.Tests;

namespace PsychologicalSupportPlatform.Edu.Infrastructure.Data.Repositories.Tests;

public class QuestionRepository: SQLRepository<DataContext, Question>, IQuestionRepository
{
    public QuestionRepository(DataContext context) : base(context)
    {
    }
}

using PsychologicalSupportPlatform.Common.Repository;
using PsychologicalSupportPlatform.Edu.Application.Interfaces.Tests.Repositories;
using PsychologicalSupportPlatform.Edu.Domain.Entities;

namespace PsychologicalSupportPlatform.Edu.Infrastructure.Data.Repositories.Tests;

public class QuestionRepository: SQLRepository<DataContext, Question>, IQuestionRepository
{
    public QuestionRepository(DataContext context) : base(context)
    {
    }
}

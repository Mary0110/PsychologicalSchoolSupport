using PsychologicalSupportPlatform.Common.Repository;
using PsychologicalSupportPlatform.Edu.Application.Interfaces.Tests;
using PsychologicalSupportPlatform.Edu.Application.Interfaces.Tests.Repositories;
using PsychologicalSupportPlatform.Edu.Domain.Entities;

namespace PsychologicalSupportPlatform.Edu.Infrastructure.Data.Repositories.Tests;

public class AnswerRepository: SQLRepository<DataContext, Answer>, IAnswerRepository
{
    public AnswerRepository(DataContext context) : base(context)
    {
    }
}

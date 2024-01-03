using PsychologicalSupportPlatform.Common.Repository;
using PsychologicalSupportPlatform.Edu.Application.Interfaces.Tests.Repositories;
using PsychologicalSupportPlatform.Edu.Domain.Entities;

namespace PsychologicalSupportPlatform.Edu.Infrastructure.Data.Repositories.Tests;

public class UserTestResultRepository: SQLRepository<DataContext, UserTestResult>, IUserTestResultRepository
{
    public UserTestResultRepository(DataContext context) : base(context)
    {
    }
}

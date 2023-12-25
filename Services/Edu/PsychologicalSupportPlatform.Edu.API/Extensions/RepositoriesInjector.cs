using PsychologicalSupportPlatform.Edu.Application.Interfaces;
using PsychologicalSupportPlatform.Edu.Application.Interfaces.Tests.Repositories;
using PsychologicalSupportPlatform.Edu.Domain.Entities;
using PsychologicalSupportPlatform.Edu.Infrastructure.Data.Repositories;
using PsychologicalSupportPlatform.Edu.Infrastructure.Data.Repositories.Tests;

namespace PsychologicalSupportPlatform.Edu.API.Extensions;

public static class RepositoriesInjector
{
    public static IServiceCollection InjectRepositories(this IServiceCollection services)
    {
        services.AddScoped<ITestRepository, TestRepository>();
        services.AddScoped<IQuestionResultRepository, QuestionResultRepository>();
        services.AddScoped<IAnswerRepository, AnswerRepository>();
        services.AddScoped<IQuestionRepository, QuestionRepository>();
        services.AddScoped<IUserTestResultRepository, UserTestResultRepository>();
        services.AddScoped<IEduMaterialRepository, EduMaterialRepository>();
        services.AddScoped<IStudentHasEduMaterialRepository, StudentHasEduMaterialRepository>();

        return services;
    }
}

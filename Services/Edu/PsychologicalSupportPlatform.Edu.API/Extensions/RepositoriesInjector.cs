using PsychologicalSupportPlatform.Edu.Application.Interfaces;
using PsychologicalSupportPlatform.Edu.Infrastructure.Data.Repositories;

namespace PsychologicalSupportPlatform.Edu.API.Extensions;

public static class RepositoriesInjector
{
    public static IServiceCollection InjectRepositories(this IServiceCollection services)
    {
        services.AddScoped<IEduMaterialRepository, EduMaterialRepository>();
        services.AddScoped<IStudentHasEduMaterialRepository, StudentHasEduMaterialRepository>();

        return services;
    }
}

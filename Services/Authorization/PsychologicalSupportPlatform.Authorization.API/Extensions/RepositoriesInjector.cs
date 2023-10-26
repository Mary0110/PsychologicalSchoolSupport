using PsychologicalSupportPlatform.Authorization.Application.Interfaces;
using PsychologicalSupportPlatform.Authorization.Infrastructure.Data;

namespace PsychologicalSupportPlatform.Authorization.API.Extensions;

public static class RepositoriesInjector
{
    public static IServiceCollection InjectRepos(this IServiceCollection services)
    {
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IStudentRepository, StudentRepository>();
        services.AddTransient<IFormRepository, FormRepository>();
        
        return services;
    }
}

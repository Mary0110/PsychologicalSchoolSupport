using PsychologicalSupportPlatform.Authorization.Application.Interfaces;
using PsychologicalSupportPlatform.Authorization.Application.Services;

namespace PsychologicalSupportPlatform.Authorization.API.Extensions;

public static class ServiceInjector
{
    public static IServiceCollection InjectServices(this IServiceCollection services)
    {
        services.AddTransient<IEncryptionService, EncryptionService>();
        services.AddTransient<ILoginService, LoginService>();
        services.AddTransient<IFormService, FormService>();

        return services;
    }
}

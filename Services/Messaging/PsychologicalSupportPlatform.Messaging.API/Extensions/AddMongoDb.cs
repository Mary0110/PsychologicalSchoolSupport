using PsychologicalSupportPlatform.Messaging.Application;
using PsychologicalSupportPlatform.Messaging.Application.Services;
using PsychologicalSupportPlatform.Messaging.Infrastructure.Data;

namespace PsychologicalSupportPlatform.Messaging.API.Extensions;

public static class AddMongoDb
{
    public static IServiceCollection AddMongoDbPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ChatDbConfig>(configuration.GetSection("MongoDB"));
        services.AddSingleton<IChatRepository, ChatRepository>();

        return services;
    }
    
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddTransient<IChatService, ChatService>();

        return services;
    }
}

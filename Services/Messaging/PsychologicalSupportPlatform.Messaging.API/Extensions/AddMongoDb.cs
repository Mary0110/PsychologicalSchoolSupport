using PsychologicalSupportPlatform.Messaging.Infrastructure.Data;

namespace PsychologicalSupportPlatform.Messaging.API.Extensions;

public static class AddMongoDb
{
    public static IServiceCollection AddMongoDbPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ChatDbConfig>(configuration.GetSection(
            key: nameof(ChatDbConfig)));
        
        return services;
    }
}
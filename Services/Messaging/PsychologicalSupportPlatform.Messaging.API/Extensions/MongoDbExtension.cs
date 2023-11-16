using PsychologicalSupportPlatform.Messaging.Application;
using PsychologicalSupportPlatform.Messaging.Infrastructure.Data;

namespace PsychologicalSupportPlatform.Messaging.API.Extensions;

public static class MongoDbExtension
{
    public static IServiceCollection AddMongoDbPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ChatDbConfig>(configuration.GetSection("MongoDB"));
        services.AddSingleton<IChatRepository, ChatRepository>();

        return services;
    }
}

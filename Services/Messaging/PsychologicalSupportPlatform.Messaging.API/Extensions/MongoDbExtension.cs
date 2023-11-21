using PsychologicalSupportPlatform.Messaging.Application;
using PsychologicalSupportPlatform.Messaging.Application.Interfaces;
using PsychologicalSupportPlatform.Messaging.Infrastructure.Data;

namespace PsychologicalSupportPlatform.Messaging.API.Extensions;

public static class MongoDbExtension
{
    public static IServiceCollection AddMongoDbPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ChatDbConfig>(configuration.GetSection("MongoDB"));
        services.AddScoped<IChatRepository, ChatRepository>();

        return services;
    }
}

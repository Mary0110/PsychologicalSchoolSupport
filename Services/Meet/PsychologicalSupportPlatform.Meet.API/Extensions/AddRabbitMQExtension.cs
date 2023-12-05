using PsychologicalSupportPlatform.Common.Config;
using PsychologicalSupportPlatform.Meet.Application.Interfaces;
using PsychologicalSupportPlatform.Meet.Infrastructure.Services;

namespace PsychologicalSupportPlatform.Meet.API.Extensions;

public static class RabbitMQAdderExtension
{
    public static IServiceCollection AddRabbitMQ(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<RabbitMQConfig>(configuration.GetSection("RabbitMQConfig"));
        services.AddTransient<IRabbitMQMessagingService, RabbitMQMessagingService>();

        return services;
    }
}

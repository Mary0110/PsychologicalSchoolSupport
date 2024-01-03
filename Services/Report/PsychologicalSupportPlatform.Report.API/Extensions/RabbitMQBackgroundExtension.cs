using PsychologicalSupportPlatform.Common.Config;
using PsychologicalSupportPlatform.Report.Application.Services;

namespace PsychologicalSupportPlatform.Report.API.Extensions;

public static class RabbitMQBackgroundExtension
{
    public static IServiceCollection AddRabbitMQBackground(this IServiceCollection services, 
        IConfiguration configuration)
    {
        services.Configure<RabbitMQConfig>(configuration.GetSection("RabbitMQConfig"));
        services.AddHostedService<RabbitMQBackgroundConsumerService>();

        return services;
    }
}

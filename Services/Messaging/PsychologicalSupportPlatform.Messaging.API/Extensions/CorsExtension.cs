using PsychologicalSupportPlatform.Messaging.Infrastructure.Config;

namespace PsychologicalSupportPlatform.Messaging.API.Extensions;

public static class CorsExtension
{
    public static IServiceCollection AddCorsPolicy(this IServiceCollection services, IConfiguration configuration)
    {
        var section = configuration.GetSection("SignalRClientConfig");
        services.Configure<SignalRConfig>(section);
        var url = section.Get<SignalRConfig>().ClientUrl;
        
        services.AddCors(options =>
        {
            options.AddPolicy("SignalRClient", builder =>
            {
                builder.WithOrigins(url)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
            });
        });
        
        return services;
    }
}

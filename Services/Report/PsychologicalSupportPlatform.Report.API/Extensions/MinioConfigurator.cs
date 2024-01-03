using Minio;
using PsychologicalSupportPlatform.Report.Application.Interfaces;
using PsychologicalSupportPlatform.Report.Infrastructure.Config;
using PsychologicalSupportPlatform.Report.Infrastructure.Data.Repositories;

namespace PsychologicalSupportPlatform.Report.API.Extensions;

public static class MinioConfigurator
{
    public static IServiceCollection ConfigureMinio(this IServiceCollection services, IConfiguration configuration)
    {
        var section = configuration.GetSection("Minio");
        services.Configure<MinioReportsConfig>(section);
        var minioConfig = section.Get<MinioReportsConfig>();
        var secure = false;
        
        services.AddScoped<IMinioClient>(_ =>
        {
            var minio = new MinioClient()
                .WithEndpoint(minioConfig.Endpoint)
                .WithCredentials(minioConfig.RootUser, minioConfig.RootPass)
                .WithSSL(secure)
                .Build();
            
            return minio;
        });

        services.AddScoped<IMinioRepository, MinioRepository>();
        
        return services;
    }
}

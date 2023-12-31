using PsychologicalSupportPlatform.Common.Interfaces;
using PsychologicalSupportPlatform.Common.Protos;
using PsychologicalSupportPlatform.Common.Services;
using PsychologicalSupportPlatform.Messaging.Application.Services;
using PsychologicalSupportPlatform.Messaging.Infrastructure.Config;

namespace PsychologicalSupportPlatform.Messaging.API.Extensions;

public static class InfrastructureServicesExtension
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IChatService, ChatService>();
        services.AddTransient<IUserGrpcClient, UserGrpcClient>();
        var section = configuration.GetSection("GrpcConfig");
        services.Configure<GrpcConfig>(section);
        var grpcConfig = section.Get<GrpcConfig>();
        services.AddGrpcServices(grpcConfig);

        return services;
    }
    
    private static IServiceCollection AddGrpcServices(this IServiceCollection services, GrpcConfig config)
    {
        services.AddGrpcClient<UserChecker.UserCheckerClient>(clientFactoryOptions =>
            {
                clientFactoryOptions.Address = new Uri(config.UsersUrl);
                clientFactoryOptions.ChannelOptionsActions.Clear();
                clientFactoryOptions.ChannelOptionsActions.Add((opt) =>
                {
                    opt.UnsafeUseInsecureChannelCallCredentials = true;
                });
            })
            .ConfigurePrimaryHttpMessageHandler(() =>
            {
                var handler = new HttpClientHandler();
                handler.ServerCertificateCustomValidationCallback =
                    HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
                return handler;
            });

        return services;
    }
}

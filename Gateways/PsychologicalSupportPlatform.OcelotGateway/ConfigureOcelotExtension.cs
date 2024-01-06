using Ocelot.DependencyInjection;

namespace PsychologicalSupportPlatform.OcelotGateway;

public static class ConfigureOcelotExtension
{
    public static IServiceCollection ConfigureOcelot(this IServiceCollection services,
        WebApplicationBuilder builder)
    {
        var path = builder.Configuration.GetSection("Ocelot:JsonPath");

        if (path.Value != null)
        {
            builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
                .AddJsonFile(path: path.Value, optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();
        }

        builder.Services.AddOcelot(builder.Configuration);
        builder.Services.AddSwaggerForOcelot(builder.Configuration);

        return services;
    }
}

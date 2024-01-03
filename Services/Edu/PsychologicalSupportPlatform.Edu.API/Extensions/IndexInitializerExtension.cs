using PsychologicalSupportPlatform.Edu.Infrastructure.Data.ElasticSearch;

namespace PsychologicalSupportPlatform.Edu.API.Extensions;

public static class IndexInitializerExtension
{
    public static async Task<IHost> InitializeElasticIndex(this IHost app, IConfiguration configuration)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<ElasticSearchIndexInitializer>();
        await context.Initialize();
        
        return app;
    }
}

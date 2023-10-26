using Microsoft.EntityFrameworkCore;
using PsychologicalSupportPlatform.Authorization.Infrastructure.Data;

namespace PsychologicalSupportPlatform.Authorization.API.Extensions;

public static class SeedDatabaseExtension
{
    public static async Task<IHost> UseDatabaseSeed(this IHost app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        
        await SeedDatabaseAsync(services);

        return app;
    }

    private static async Task SeedDatabaseAsync(IServiceProvider services)
    {
        var context = services.GetRequiredService<DataContext>();

        await DbInitializer.SeedData(context);
    }
}

using Microsoft.EntityFrameworkCore;
using PsychologicalSupportPlatform.Report.Infrastracture.Data;

namespace PsychologicalSupportPlatform.Report.API.Extensions;

public static class DbContextAdder
{
    public static IServiceCollection AddDatabaseContext(this IServiceCollection services,  IConfiguration configuration)
    {
        services.AddDbContext<DataContext>(
            optionsBuilder => optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                dbContextOptionsBuilder => dbContextOptionsBuilder.MigrationsAssembly(configuration.GetSection("MigrationsAssembly").Get<string>())));
        
        return services;
    }
}

using Microsoft.EntityFrameworkCore;
using PsychologicalSupportPlatform.Edu.Infrastructure.Data;

namespace PsychologicalSupportPlatform.Edu.API.Extensions;

public static class DbContextAdder
{
    public static IServiceCollection AddDatabaseContext(this IServiceCollection services,  IConfiguration configuration)
    {
        services.AddDbContext<DataContext>(
            optionsBuilder => 
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                sqlServerDbContextOptionsBuilder => 
                    sqlServerDbContextOptionsBuilder.MigrationsAssembly(
                        configuration.GetSection("MigrationsAssembly").Get<string>())
                    )
                );
        
        return services;
    }
}

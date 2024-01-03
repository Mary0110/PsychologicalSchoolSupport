using Microsoft.EntityFrameworkCore;
using PsychologicalSupportPlatform.Authorization.Infrastructure.Data;

namespace PsychologicalSupportPlatform.Authorization.API.Extensions;

public static class DbContextAdder
{
    public static IServiceCollection AddDatabaseContext(this IServiceCollection services,  IConfiguration conf)
    {
        services.AddDbContext<DataContext>(
            opt => opt.UseSqlServer(conf.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(
                    conf.GetSection("MigrationsAssembly").Get<string>())));
        
        return services;
    }
}

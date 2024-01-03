using Microsoft.EntityFrameworkCore;
using PsychologicalSupportPlatform.Common.Config;
using PsychologicalSupportPlatform.Edu.Infrastructure.Config;
using PsychologicalSupportPlatform.Edu.Infrastructure.Data;

namespace PsychologicalSupportPlatform.Edu.API.Extensions;

public static class DbAdder
{
    public static IServiceCollection AddDatabaseContext(this IServiceCollection services, IConfiguration configuration)
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
    
    public static IServiceCollection AddRedis(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddStackExchangeRedisCache(redisOptions =>
        {
            string connection = configuration.GetConnectionString("Redis");
            redisOptions.InstanceName = "EduMaterials";
            redisOptions.Configuration = connection;
        });
        
        var section = configuration.GetSection("RedisConfig");
        services.Configure<RedisConfig>(section);
        
        return services;
    }
}

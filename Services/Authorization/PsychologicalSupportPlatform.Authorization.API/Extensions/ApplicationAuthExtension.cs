using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using PsychologicalSupportPlatform.Common.Config;

namespace PsychologicalSupportPlatform.Authorization.API.Extensions;

public static class ApplicationAuthExtension
{
    public static IServiceCollection AddAuthenticate(this IServiceCollection services, IConfiguration conf)
    {
        var section = conf.GetSection("Auth");
        services.Configure<AuthOptions>(section);
        var authOptions = section.Get<AuthOptions>();
        
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = authOptions.Issuer,
                    ValidateAudience = true,
                    ValidAudience = authOptions.Audience,
                    ValidateLifetime = true,
                    IssuerSigningKey = authOptions.GetSymmetricSecurityKey(),
                    ValidateIssuerSigningKey = true
                };
            });
        
        return services;
    }
}

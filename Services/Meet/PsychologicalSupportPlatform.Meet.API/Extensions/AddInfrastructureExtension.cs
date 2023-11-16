using PsychologicalSupportPlatform.Meet.Domain.Interfaces;
using PsychologicalSupportPlatform.Meet.Infrastructure.Data.Repositories;

namespace PsychologicalSupportPlatform.Meet.API.Extensions;

public static class InjectRepositories
{
    public static IServiceCollection InjectRepos(this IServiceCollection services)
    {
        services.AddTransient(typeof(IMeetupRepository), typeof(MeetupRepository));
        services.AddTransient(typeof(IOpeningRepository), typeof(OpeningRepository));
        
        return services;
    }
}
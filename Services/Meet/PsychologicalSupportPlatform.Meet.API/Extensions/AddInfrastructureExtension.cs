using PsychologicalSupportPlatform.Meet.Domain.Interfaces;
using PsychologicalSupportPlatform.Meet.Infrastructure.Data.Repositories;

namespace PsychologicalSupportPlatform.Meet.API.Extensions;

public static class AddInfrastructureExtension
{
    public static IServiceCollection InjectRepositories(this IServiceCollection services)
    {
        services.AddTransient(typeof(IMeetupRepository), typeof(MeetupRepository));
        services.AddTransient(typeof(IScheduleCellRepository), typeof(ScheduleCellRepository));
        
        return services;
    }
}
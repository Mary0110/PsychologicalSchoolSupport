using PsychologicalSupportPlatform.Report.Application.Interfaces;
using PsychologicalSupportPlatform.Report.Infrastracture.Data;

namespace PsychologicalSupportPlatform.Report.API.Extensions;

public static class RepositoriesInjector
{
    public static IServiceCollection InjectRepositories(this IServiceCollection services)
    {
        services.AddScoped<IReportRepository, ReportRepository>();

        return services;
    }
}

using PsychologicalSupportPlatform.Report.Application.Interfaces;
using PsychologicalSupportPlatform.Report.Domain.Entities;
using PsychologicalSupportPlatform.Report.Infrastructure.Data;
using PsychologicalSupportPlatform.Report.Infrastructure.Data.Repositories;

namespace PsychologicalSupportPlatform.Report.API.Extensions;

public static class RepositoriesInjector
{
    public static IServiceCollection InjectRepositories(this IServiceCollection services)
    {
        services.AddScoped<IReportRepository, ReportRepository>();
        services.AddScoped<IMonthlyReportRepository, MonthlyReportRepository>();

        return services;
    }
}
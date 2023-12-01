using PsychologicalSupportPlatform.Report.Application.Interfaces;
using PsychologicalSupportPlatform.Report.Application.Services;

namespace PsychologicalSupportPlatform.Report.API.Extensions;

public static class ServiceInjector
{
    public static IServiceCollection InjectServices(this IServiceCollection services)
    {
        services.AddTransient<IGenerateReportService, GenerateReportService>();
        services.AddTransient<IReportService, ReportService>();

        return services;
    }
}

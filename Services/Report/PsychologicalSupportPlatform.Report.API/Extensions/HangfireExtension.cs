using Hangfire;
using PsychologicalSupportPlatform.Report.Application.Interfaces;
using PsychologicalSupportPlatform.Report.Application.Services;

namespace PsychologicalSupportPlatform.Report.API.Extensions;

public static class HangfireExtension
{
    public static IServiceCollection AddHangfireService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IMonthlyReportService, MonthlyReportService>();
   
        services.AddHangfire(config => config
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseSqlServerStorage(configuration.GetConnectionString("HangfireConnection")));
        
        services.AddHangfireServer();
        
        return services;
    }
    
    public static IApplicationBuilder StartRecurringJob(this IApplicationBuilder app)
    {
        app.UseHangfireDashboard();
        RecurringJob.AddOrUpdate<IMonthlyReportService>("monthly-report-job", monthlyReportService => monthlyReportService.AddMonthlyReportAsync(), Cron.Monthly);
        
        return app;
    }
}

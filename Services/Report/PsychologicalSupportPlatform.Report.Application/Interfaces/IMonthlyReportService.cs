using PsychologicalSupportPlatform.Report.Application.DTOs;

namespace PsychologicalSupportPlatform.Report.Application.Interfaces;

public interface IMonthlyReportService
{
    Task<int> AddMonthlyReportAsync();
    
    // Task<MemoryStream> GetMonthlyReportAsync(MonthlyReportDTO dto, CancellationToken token);
}

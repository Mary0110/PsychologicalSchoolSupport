namespace PsychologicalSupportPlatform.Report.Application.Interfaces;

public interface IReportService
{
    Task<List<QuarterReport>> GetAllQuarterReportsAsync(int pageNumber, int pageSize);
    
    Task<QuarterReport?> GetQuarterReportAsync(int parallel, char letter);

    Task<List<QuarterReport>> GetQuarterReportsFromDateAsync(DateTime date, int pageNumber, int pageSize);
    
    Task DeleteQuarterReportAsync(QuarterReport form);

    Task EditQuarterReportAsync(QuarterReport form);

    Task AddQuarterReportAsync(QuarterReport form);
}
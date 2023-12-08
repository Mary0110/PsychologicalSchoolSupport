using PsychologicalSupportPlatform.Report.Application.DTOs;

namespace PsychologicalSupportPlatform.Report.Application.Interfaces;

public interface IGenerateMonthlyReportService
{
    public MemoryStream GenerateMonthlyReport(List<ReportDTO> reportMeetupDtos);
}

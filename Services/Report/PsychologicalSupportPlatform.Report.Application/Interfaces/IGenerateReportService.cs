using PsychologicalSupportPlatform.Common;

namespace PsychologicalSupportPlatform.Report.Application.Interfaces;

public interface IGenerateReportService
{
    public MemoryStream GenerateReport(ReportMeetupDTO reportMeetupDtos);
}

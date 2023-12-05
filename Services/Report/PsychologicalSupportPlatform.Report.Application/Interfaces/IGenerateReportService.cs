using PsychologicalSupportPlatform.Common;
using PsychologicalSupportPlatform.Report.Application.DTOs;

namespace PsychologicalSupportPlatform.Report.Application.Interfaces;

public interface IGenerateReportService
{
    public MemoryStream GenerateReport(ReportMeetupDTO reportMeetupDtos);
}

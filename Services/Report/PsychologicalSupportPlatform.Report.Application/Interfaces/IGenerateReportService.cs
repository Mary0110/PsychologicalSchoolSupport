using PsychologicalSupportPlatform.Common;
using PsychologicalSupportPlatform.Report.Application.DTOs;

namespace PsychologicalSupportPlatform.Report.Application.Interfaces;

public interface IGenerateReportService
{
    public void GenerateReport(string filePath, string creatorName, ReportMeetupDTO reportMeetupDtos);
}

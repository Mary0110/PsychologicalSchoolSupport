using PsychologicalSupportPlatform.Common;
using PsychologicalSupportPlatform.Report.Application.DTOs;

namespace PsychologicalSupportPlatform.Report.Application.Interfaces;

public interface IReportService
{
    Task<int> AddMeetupReportAsync(MeetupMessageObject form);
    
    Task<MemoryStream> GenerateReportAsync(GenerateReportDTO dto, CancellationToken token);
}

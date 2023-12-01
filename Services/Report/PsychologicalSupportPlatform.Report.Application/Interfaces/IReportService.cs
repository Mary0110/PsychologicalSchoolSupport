using PsychologicalSupportPlatform.Common;
using PsychologicalSupportPlatform.Report.Application.DTOs;
using PsychologicalSupportPlatform.Report.Domain.Entities;

namespace PsychologicalSupportPlatform.Report.Application.Interfaces;

public interface IReportService
{
    Task<List<MeetupReportDTO>> GetAllMeetupReportsAsync(int pageNumber, int pageSize);
    
    Task<MeetupReportDTO?> GetMeetupReportAsync(int id);
    
    Task<List<MeetupReportDTO>> GetMeetupReportsByCreatorIdAsync(int creatorId, int pageNumber, int pageSize);

    Task<List<MeetupReportDTO>> GetMeetupReportsByDateAsync(DateTime date, int pageNumber, int pageSize);
    
    Task<int> DeleteMeetupReportAsync(int id);

    Task EditMeetupReportAsync(MeetupReportDTO form);

    Task<int> AddMeetupReportAsync(MeetupMessageObject form);
    
    Task GenerateReportAsync(int meetId, string comments, string id);
}

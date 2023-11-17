using PsychologicalSupportPlatform.Common.Repository;
using PsychologicalSupportPlatform.Meet.Domain.Entities;

namespace PsychologicalSupportPlatform.Meet.Domain.Interfaces;

public interface IMeetupRepository : ISQLRepository<Meetup>
{
    Task<List<Meetup>> GetMeetingsByStudentIdAsync(int id, int pageNumber, int pageSize);
    
    Task<List<Meetup>> GetMeetingsByDateAsync(DateOnly date, int pageNumber, int pageSize);
}

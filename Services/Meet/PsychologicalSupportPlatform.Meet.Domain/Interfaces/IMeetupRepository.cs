using PsychologicalSupportPlatform.Meet.Domain.Entities;

namespace PsychologicalSupportPlatform.Meet.Domain.Interfaces;

public interface IMeetupRepository
{
    Task<Meetup?> GetMeetingByIdAsync(int id);
    
    Task<IReadOnlyList<Meetup?>> GetAllMeetingsAsync();
    
    Task<IReadOnlyList<Meetup?>> GetMeetingsByStudentIdAsync(int id);

    Task<IReadOnlyList<Meetup?>> GetMeetingsByDateAsync(DateTime date);

    Task AddMeetingAsync(Meetup meet);
    
    Task UpdateMeetingAsync(Meetup meet);
    
    Task DeleteMeetingAsync(Meetup meet);
    
    Task SaveMeetingAsync();   
}

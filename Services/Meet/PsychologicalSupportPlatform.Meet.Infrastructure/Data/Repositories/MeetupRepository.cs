using PsychologicalSupportPlatform.Common.Repository;
using PsychologicalSupportPlatform.Meet.Domain.Entities;
using PsychologicalSupportPlatform.Meet.Domain.Interfaces;

namespace PsychologicalSupportPlatform.Meet.Infrastructure.Data.Repositories;

public class MeetupRepository: SQLRepository<DataContext, Meetup>, IMeetupRepository
{
    public MeetupRepository(DataContext context) : base(context) { }
    
    public async Task<List<Meetup>> GetMeetingsByStudentIdAsync(int id, int pageNumber, int pageSize)
    {
        return await GetAllAsync(p => p.StudentId == id, pageNumber, pageSize);
    }

    public async Task<List<Meetup>> GetMeetingsByDateAsync(DateOnly date, int pageNumber, int pageSize)
    {
        return await GetAllAsync(p => p.Date == date, pageNumber, pageSize);
    }
}

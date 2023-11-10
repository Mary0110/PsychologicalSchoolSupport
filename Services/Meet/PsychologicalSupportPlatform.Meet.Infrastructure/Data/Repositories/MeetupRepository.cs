using Microsoft.EntityFrameworkCore;
using PsychologicalSupportPlatform.Meet.Domain.Entities;
using PsychologicalSupportPlatform.Meet.Domain.Interfaces;

namespace PsychologicalSupportPlatform.Meet.Infrastructure.Data.Repositories;

public class MeetupRepository: IMeetupRepository
{
    private readonly DataContext context;
    
    public MeetupRepository(DataContext context)
    {
        this.context = context;
    }
    
    public async Task<Meetup?> GetMeetingByIdAsync(int id)
    {
        return await context.Meetups.FindAsync(id);
    }

    public async Task<IReadOnlyList<Meetup?>> GetAllMeetingsAsync()
    {
        return await context.Meetups.ToListAsync();
    }

    public async Task<IReadOnlyList<Meetup?>> GetMeetingsByStudentIdAsync(int id)
    {
        return context.Meetups.AsNoTracking().Where(p => p.StudentId == id).ToList();
    }

    public async Task<IReadOnlyList<Meetup?>> GetMeetingsByDateAsync(DateOnly date)
    {
        return context.Meetups.AsNoTracking().Where(p => p.Date == date).ToList();
    }

    public async Task AddMeetingAsync(Meetup meet)
    {
        Console.WriteLine($"meet  {meet.OpeningId}");
        await context.Meetups.AddAsync(meet);
    }

    public async Task UpdateMeetingAsync(Meetup meet)
    {
        context.Entry(meet).State = EntityState.Modified;
        await context.SaveChangesAsync();    
    }

    public async Task DeleteMeetingAsync(Meetup meet)
    {
        context.Meetups.Remove(meet);
        await context.SaveChangesAsync();    
    }

    public async Task SaveMeetingAsync()
    {
        await context.SaveChangesAsync();
    }
}

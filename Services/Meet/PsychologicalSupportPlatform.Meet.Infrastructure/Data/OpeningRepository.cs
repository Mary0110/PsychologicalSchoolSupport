using Microsoft.EntityFrameworkCore;
using PsychologicalSupportPlatform.Meet.Domain.Entities;
using PsychologicalSupportPlatform.Meet.Domain.Interfaces;

namespace PsychologicalSupportPlatform.Meet.Infrastructure.Data;

public class OpeningRepository : IOpeningRepository
{
    private readonly DataContext context;
    
    public OpeningRepository(DataContext context)
    {
        this.context = context;
    }
    
    public async Task<Opening?> GetOpeningByIdAsync(int id)
    {
        return await context.Openings.Include(o => o.Meetups).AsNoTracking().FirstOrDefaultAsync(o =>o.Id == id);
    }

    public async Task<IReadOnlyList<Opening>> GetAllOpeningsAsync()
    {
        return await context.Openings.ToListAsync();
    }

    public async Task<IReadOnlyList<Opening>> GetOpeningsByDayOfWeekAsync(DayOfWeek dayOfWeek)
    {
        return context.Openings.AsNoTracking().Where(p => p.Day == dayOfWeek).ToList();
    }
    
    public async Task<IReadOnlyList<Opening>> GetOpeningsByDayAndTimeAsync(DayOfWeek dayOfWeek, TimeOnly time)
    {
        return context.Openings.AsNoTracking().Where(p => p.Day == dayOfWeek && p.Time == time).ToList();
    }

    public async Task<IReadOnlyList<Opening>> GetOpeningsByStatusAsync(bool status)
    {
        return context.Openings.AsNoTracking().Where(p => p.Active == status).ToList();
    }
    
    public async Task<IReadOnlyList<Opening>> GetAvailableOpeningsAsync()
    {
        return context.Openings.AsNoTracking().Where(p => p.Active && p.Time >= TimeOnly.FromDateTime(DateTime.Now)).Include(p => p.Meetups)
            .Where(p => p.Meetups.Any(c => c.Date >= DateOnly.FromDateTime(DateTime.Now.Date))).ToList();
    }

    public async Task AddOpeningsAsync(Opening opening)
    {
        await context.Openings.AddAsync(opening);
    }

    public async Task UpdateOpeningsAsync(Opening opening)
    {
        context.Entry(opening).State = EntityState.Modified;
        await context.SaveChangesAsync();        
    }

    public async Task DeleteOpeningsAsync(Opening opening)
    {
        context.Openings.Remove(opening);
        await context.SaveChangesAsync();        
    }

    public async Task SaveOpeningsAsync()
    {
        await context.SaveChangesAsync();
    }
}

using Microsoft.EntityFrameworkCore;
using PsychologicalSupportPlatform.Meet.Domain.Entities;
using PsychologicalSupportPlatform.Meet.Domain.Interfaces;

namespace PsychologicalSupportPlatform.Meet.Infrastructure.Data.Repositories;

public class ScheduleCellRepository : IScheduleCellRepository
{
    private readonly DataContext context;
    
    public ScheduleCellRepository(DataContext context)
    {
        this.context = context;
    }
    
    public async Task<ScheduleCell?> GetScheduleCellByIdAsync(int id)
    {
        return await context.ScheduleCells.Include(o => o.Meetups).AsNoTracking().FirstOrDefaultAsync(o =>o.Id == id);
    }

    public async Task<IReadOnlyList<ScheduleCell>> GetAllScheduleCellsAsync()
    {
        return await context.ScheduleCells.ToListAsync();
    }

    public async Task<IReadOnlyList<ScheduleCell>> GetScheduleCellsByDayOfWeekAsync(DayOfWeek dayOfWeek)
    {
        return context.ScheduleCells.AsNoTracking().Where(p => p.Day == dayOfWeek).ToList();
    }
    
    public async Task<IReadOnlyList<ScheduleCell>> GetScheduleCellsByDayAndTimeAsync(DayOfWeek dayOfWeek, TimeOnly time)
    {
        return context.ScheduleCells.AsNoTracking().Where(p => p.Day == dayOfWeek && p.Time == time).ToList();
    }

    public async Task<IReadOnlyList<ScheduleCell>> GetScheduleCellsByStatusAsync(bool status)
    {
        return context.ScheduleCells.AsNoTracking().Where(p => p.Active == status).ToList();
    }
    
    public async Task<IReadOnlyList<ScheduleCell>> GetAvailableScheduleCellsAsync()
    {
        return context.ScheduleCells.AsNoTracking().Where(p => p.Active && p.Time >= TimeOnly.FromDateTime(DateTime.Now)).Include(p => p.Meetups)
            .Where(p => p.Meetups.Any(c => c.Date >= DateOnly.FromDateTime(DateTime.Now.Date))).ToList();
    }

    public async Task AddScheduleCellsAsync(ScheduleCell scheduleCell)
    {
        await context.ScheduleCells.AddAsync(scheduleCell);
    }

    public async Task UpdateScheduleCellsAsync(ScheduleCell scheduleCell)
    {
        context.Entry(scheduleCell).State = EntityState.Modified;
        await context.SaveChangesAsync();        
    }

    public async Task DeleteScheduleCellsAsync(ScheduleCell scheduleCell)
    {
        context.ScheduleCells.Remove(scheduleCell);
        await context.SaveChangesAsync();        
    }

    public async Task SaveScheduleCellsAsync()
    {
        await context.SaveChangesAsync();
    }
}

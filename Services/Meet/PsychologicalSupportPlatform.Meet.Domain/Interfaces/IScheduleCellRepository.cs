using PsychologicalSupportPlatform.Meet.Domain.Entities;

namespace PsychologicalSupportPlatform.Meet.Domain.Interfaces;

public interface IScheduleCellRepository
{
    Task<ScheduleCell?> GetScheduleCellByIdAsync(int id);
    
    Task<IReadOnlyList<ScheduleCell>> GetAllScheduleCellsAsync();
    
    Task<IReadOnlyList<ScheduleCell>> GetScheduleCellsByDayOfWeekAsync(DayOfWeek dayOfWeek);
    
    Task<IReadOnlyList<ScheduleCell>> GetScheduleCellsByStatusAsync(bool status);
    
    Task<IReadOnlyList<ScheduleCell>> GetAvailableScheduleCellsAsync();

    Task AddScheduleCellsAsync(ScheduleCell scheduleCell);
    
    Task UpdateScheduleCellsAsync(ScheduleCell scheduleCell);
    
    Task DeleteScheduleCellsAsync(ScheduleCell scheduleCell);

    Task<IReadOnlyList<ScheduleCell>> GetScheduleCellsByDayAndTimeAsync(DayOfWeek dayOfWeek, TimeOnly time);

    Task SaveScheduleCellsAsync();   
}

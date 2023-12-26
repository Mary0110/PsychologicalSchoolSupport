using PsychologicalSupportPlatform.Common.Repository;
using PsychologicalSupportPlatform.Meet.Domain.Entities;

namespace PsychologicalSupportPlatform.Meet.Domain.Interfaces;

public interface IScheduleCellRepository : ISQLRepository<ScheduleCell>
{
    Task<List<ScheduleCell>> GetScheduleCellsByDayOfWeekAsync(DayOfWeek dayOfWeek, int pageNumber, int pageSize);
    
    Task<List<ScheduleCell>> GetScheduleCellsByStatusAsync(bool status, int pageNumber, int pageSize);
    
    Task<List<ScheduleCell>> GetScheduleCellsByDayAndTimeAsync(DayOfWeek dayOfWeek, TimeOnly time, int psychologistId);

    Task<List<ScheduleCell>> GetAvailableScheduleCellsAsync(int pageNumber, int pageSize);
}

using PsychologicalSupportPlatform.Common.Repository;
using PsychologicalSupportPlatform.Meet.Domain.Entities;
using SpecificationPatternDotNet;

namespace PsychologicalSupportPlatform.Meet.Domain.Interfaces;

public interface IScheduleCellRepository : ISQLRepository<ScheduleCell>
{
    Task<List<ScheduleCell>> GetScheduleCellsByDayOfWeekAsync(DayOfWeek dayOfWeek, int pageNumber, int pageSize);
    
    Task<List<ScheduleCell>> GetScheduleCellsByStatusAsync(bool status, int pageNumber, int pageSize);
    
    Task<List<ScheduleCell>> GetScheduleCellsByDayAndTimeAsync(DayOfWeek dayOfWeek, TimeOnly time);

    Task<List<ScheduleCell>> GetAvailableScheduleCellsAsync(int pageNumber, int pageSize);
}

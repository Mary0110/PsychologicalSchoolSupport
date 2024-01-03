using PsychologicalSupportPlatform.Common.Repository;
using PsychologicalSupportPlatform.Meet.Domain.Entities;
using PsychologicalSupportPlatform.Meet.Domain.Interfaces;
using PsychologicalSupportPlatform.Meet.Infrastructure.Data.Specifications;

namespace PsychologicalSupportPlatform.Meet.Infrastructure.Data.Repositories;

public class ScheduleCellRepository : SQLRepository<DataContext, ScheduleCell>, IScheduleCellRepository
{
    public ScheduleCellRepository(DataContext context) : base(context) { }
    
    public async Task<List<ScheduleCell>> GetScheduleCellsByDayOfWeekAsync(DayOfWeek dayOfWeek, int pageNumber, int pageSize)
    {
        return await GetAllAsync(cell => cell.Day == dayOfWeek, pageNumber, pageSize);
    }

    public async Task<List<ScheduleCell>> GetScheduleCellsByStatusAsync(bool status, int pageNumber, int pageSize)
    {
        return await GetAllAsync(cell => cell.Active == status, pageNumber, pageSize);
    }

    public async Task<List<ScheduleCell>> GetScheduleCellsByDayAndTimeAsync(DayOfWeek dayOfWeek, TimeOnly time, int psychologistId)
    {
        return await GetAllAsync(cell => cell.Day == dayOfWeek && cell.Time == time
        && cell.PsychologistId == psychologistId);
    }

    public async Task<List<ScheduleCell>> GetAvailableScheduleCellsAsync(int pageNumber, int pageSize)
    {
        var specification = new AvailableScheduleCellSpecification();
        
        return await GetBySpecificationAsync(specification, pageNumber, pageSize);
    }
}

using PsychologicalSupportPlatform.Meet.Domain.Entities;

namespace PsychologicalSupportPlatform.Meet.Domain.Interfaces;

public interface IOpeningRepository
{
    Task<Opening?> GetOpeningByIdAsync(int id);
    
    Task<IReadOnlyList<Opening>> GetAllOpeningsAsync();
    
    Task<IReadOnlyList<Opening>> GetOpeningsByDayOfWeekAsync(DayOfWeek dayOfWeek);
    
    Task<IReadOnlyList<Opening>> GetOpeningsByStatusAsync(bool status);
    
    Task AddOpeningsAsync(Opening opening);
    
    Task UpdateOpeningsAsync(Opening opening);
    
    Task DeleteOpeningsAsync(Opening opening);
    
    Task SaveOpeningsAsync();   
}

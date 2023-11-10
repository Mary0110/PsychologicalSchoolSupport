using PsychologicalSupportPlatform.Messaging.Domain.Entities;

namespace PsychologicalSupportPlatform.Messaging.Application;

public interface IMessageRepository
{
    public Task<List<Message>> GetAsync() ;
    
    public Task<Message?> GetAsync (int mesId) ;
    
    public Task AddAsync (Message newMes) ;
        
    public Task UpdateAsync(int id, Message updatedMes);
        
    public Task RemoveAsync(int id);
}
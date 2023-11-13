using PsychologicalSupportPlatform.Messaging.Domain.Entities;

namespace PsychologicalSupportPlatform.Messaging.Application;

public interface IMessageRepository
{
    public Task<List<Message>> GetAsync() ;
    
    public Task<Message?> GetAsync (string mesId) ;
    
    public Task AddAsync (Message newMes) ;
        
    public Task UpdateAsync(string id, Message updatedMes);
        
    public Task RemoveAsync(string id);
}
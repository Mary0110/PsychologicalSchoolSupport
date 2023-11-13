using PsychologicalSupportPlatform.Messaging.Domain.Entities;

namespace PsychologicalSupportPlatform.Messaging.Application;

public interface IChatRepository
{
    Task<Message?> GetAsync (string mesId);
    
    Task AddAsync (Message newMes);
    
    Task UpdateAsync(string id, Message updatedMes);
        
    Task RemoveAsync(string id);
    
    Task<List<Message>> GetChatHistoryAsync(string getLoggedInUserId, string otherUserId);
}

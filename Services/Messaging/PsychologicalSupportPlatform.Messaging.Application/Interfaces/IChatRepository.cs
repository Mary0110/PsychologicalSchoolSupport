using PsychologicalSupportPlatform.Messaging.Domain.Entities;

namespace PsychologicalSupportPlatform.Messaging.Application.Interfaces;

public interface IChatRepository
{
    Task<Message?> GetAsync (string mesId);
    
    Task AddAsync (Message newMes, CancellationToken token);
    
    Task UpdateAsync(string id, Message updatedMes);
        
    Task RemoveAsync(string id);
    
    Task<List<Message>> GetChatHistoryAsync(string getLoggedInUserId, string otherUserId, int pageNumber, int pageSize, CancellationToken token);
}

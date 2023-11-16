using PsychologicalSupportPlatform.Messaging.Application.DTOs;
using PsychologicalSupportPlatform.Messaging.Domain.Entities;

namespace PsychologicalSupportPlatform.Messaging.Application.Services;

public interface IChatService
{
    Task AddMessageAsync(AddMessageDTO messageDTO);

    Task<Message?> GetAsync(string mesId);
    
    Task<List<Message>> GetAllChatHistoryAsync(string curUserId, string otherUserId);
}

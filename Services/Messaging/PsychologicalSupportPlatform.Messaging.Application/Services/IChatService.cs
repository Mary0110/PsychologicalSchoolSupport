using PsychologicalSupportPlatform.Messaging.Domain.Entities;

namespace PsychologicalSupportPlatform.Messaging.Application.Services;

public interface IChatService
{
    Task<Message> AddMessageAsync(Message message);

    Task<Message?> GetAsync(string mesId);
    Task<List<Message>> GetAllChatHistoryAsync(string curUserId, string otherUserId);
}

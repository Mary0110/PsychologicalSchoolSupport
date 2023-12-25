using PsychologicalSupportPlatform.Messaging.Application.DTOs;
using PsychologicalSupportPlatform.Messaging.Domain.Entities;

namespace PsychologicalSupportPlatform.Messaging.Application.Services;

public interface IChatService
{
    Task AddMessageAsync(AddMessageDTO messageDTO, CancellationToken token);
    
    Task<List<AddMessageDTO>> GetAllChatHistoryAsync(GetChatHistoryDTO dto, int pageNumber, int pageSize, CancellationToken token);
}

using PsychologicalSupportPlatform.Common.Errors;
using PsychologicalSupportPlatform.Messaging.Domain.Entities;

namespace PsychologicalSupportPlatform.Messaging.Application.Services;

public class ChatService : IChatService
{
    private readonly IChatRepository repository;

    public ChatService(IChatRepository repository)
    {
        this.repository = repository;
    }

    public async Task<Message> AddMessageAsync(Message message)
    {
        if (message is null)
        {
            throw new WrongRequestDataException();
        }
        
        var oldMes = await repository.GetAsync(message.Id);
        
        if (oldMes is not null)
        {
            throw new AlreadyExistsException();
        }
        
        await repository.AddAsync(message);
        
        return message;
    }

    public async Task<Message?> GetAsync(string roomId)
    {
        var mes = await repository.GetAsync(roomId);

        if (mes is null)
        {
            throw new EntityNotFoundException(nameof(roomId));
        }

        return mes;
    }

    public async Task<List<Message>> GetAllChatHistoryAsync(string curUserId, string otherUserId)
    {
        var messsages = await repository.GetChatHistoryAsync(curUserId, otherUserId);

        return messsages;    
    }
}

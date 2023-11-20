using PsychologicalSupportPlatform.Common;
using PsychologicalSupportPlatform.Common.Errors;
using PsychologicalSupportPlatform.Common.Interfaces;
using PsychologicalSupportPlatform.Messaging.Application.DTOs;
using PsychologicalSupportPlatform.Messaging.Application.Errors;
using PsychologicalSupportPlatform.Messaging.Domain.Entities;

namespace PsychologicalSupportPlatform.Messaging.Application.Services;

public class ChatService : IChatService
{
    private readonly IChatRepository repository;
    private readonly IUserGrpcClient userGrpcClient;

    
    public ChatService(IChatRepository repository, IUserGrpcClient userGrpcClient)
    {
        this.repository = repository;
        this.userGrpcClient = userGrpcClient;
    }

    public async Task AddMessageAsync(AddMessageDTO messageDTO)
    {
        if (messageDTO is null)
        {
            throw new WrongRequestDataException();
        }
        
        var sender = await userGrpcClient.CheckUserAsync(int.Parse(messageDTO.SenderId));
        var senderRole = sender.Role;
        var consumer = await userGrpcClient.CheckUserAsync(int.Parse(messageDTO.ConsumerId));
        var consumerRole = consumer.Role;

        if (!(senderRole == Roles.Student && consumerRole == Roles.Psychologist ||
            consumerRole == Roles.Student && senderRole == Roles.Psychologist))
        {
            throw new WrongRolesForSendingMessages(senderRole, consumerRole);
        }

        var message = new Message(
            messageDTO.ConsumerId, messageDTO.SenderId, messageDTO.Text, messageDTO.DateTime
            );
        await repository.AddAsync(message);
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
        var user = await userGrpcClient.CheckUserAsync(int.Parse(otherUserId));
        var messsages = await repository.GetChatHistoryAsync(curUserId, otherUserId);

        return messsages;    
    }
}

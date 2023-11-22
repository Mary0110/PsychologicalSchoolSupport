using MapsterMapper;
using PsychologicalSupportPlatform.Common;
using PsychologicalSupportPlatform.Common.Errors;
using PsychologicalSupportPlatform.Common.Interfaces;
using PsychologicalSupportPlatform.Messaging.Application.DTOs;
using PsychologicalSupportPlatform.Messaging.Application.Errors;
using PsychologicalSupportPlatform.Messaging.Application.Interfaces;
using PsychologicalSupportPlatform.Messaging.Domain.Entities;

namespace PsychologicalSupportPlatform.Messaging.Application.Services;

public class ChatService : IChatService
{
    private readonly IChatRepository _repository;
    private readonly IUserGrpcClient _userGrpcClient;
    private readonly IMapper _mapper;

    public ChatService(IChatRepository repository, IUserGrpcClient userGrpcClient, IMapper mapper)
    {
        _repository = repository;
        _userGrpcClient = userGrpcClient;
        _mapper = mapper;
    }

    public async Task AddMessageAsync(AddMessageDTO messageDTO, CancellationToken token)
    {
        if (messageDTO is null)
        {
            throw new WrongRequestDataException(nameof(messageDTO));
        }
        
        if (messageDTO.ConsumerId is null)
        {
            throw new WrongRequestDataException(nameof(messageDTO.ConsumerId));
        }
        
        if (messageDTO.SenderId is null)
        {
            throw new WrongRequestDataException(nameof(messageDTO.SenderId));
        }
        
        var sender = await _userGrpcClient.CheckUserAsync(int.Parse(messageDTO.SenderId), token);
        var senderRole = sender.Role;
        var consumer = await _userGrpcClient.CheckUserAsync(int.Parse(messageDTO.ConsumerId), token);

        var consumerRole = consumer.Role;

        if (!(senderRole == Roles.Student && consumerRole == Roles.Psychologist ||
            consumerRole == Roles.Student && senderRole == Roles.Psychologist))
        {
            throw new WrongRolesForSendingMessages(senderRole, consumerRole);
        }

        var message = new Message(
            messageDTO.ConsumerId, messageDTO.SenderId, messageDTO.Text, messageDTO.DateTime
            );
        await _repository.AddAsync(message, token);
    }

    public async Task<List<AddMessageDTO>> GetAllChatHistoryAsync(string curUserId, string otherUserId, int pageNumber, int pageSize, CancellationToken token)
    {
        var user = await _userGrpcClient.CheckUserAsync(int.Parse(otherUserId), token);
        var messages = await _repository.GetChatHistoryAsync(curUserId, otherUserId, pageNumber, pageSize, token);
        var messageDTOs = _mapper.Map<List<AddMessageDTO>>(messages);
        
        return messageDTOs;    
    }
}

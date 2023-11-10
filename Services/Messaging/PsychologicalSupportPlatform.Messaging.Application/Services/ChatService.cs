using MapsterMapper;
using PsychologicalSupportPlatform.Common;
using PsychologicalSupportPlatform.Messaging.Domain.DTOs;
using PsychologicalSupportPlatform.Messaging.Domain.Entities;

namespace PsychologicalSupportPlatform.Messaging.Application.Services;

public class ChatService : IChatService
{
    private readonly IMapper mapper;
    private readonly IMessageRepository repository;

    public ChatService(IMessageRepository repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    public async Task<ResponseInfo> SendMessage(AddMessageDTO mesDTO)
    {
        if (mesDTO is null) return new ResponseInfo(success: false, message: "wrong request data");

        var newMesDTO = new MessageDTO(mesDTO.SenderId, mesDTO.ConsumerId, mesDTO.Text, DateTime.Now);
        
        var newMes = mapper.Map<Message>(newMesDTO);

        await repository.AddAsync(newMes);
        
        return new ResponseInfo(success: true, message: "mes saved");    
    }
}
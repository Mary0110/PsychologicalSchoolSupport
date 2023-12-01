using Mapster;
using PsychologicalSupportPlatform.Messaging.Application.DTOs;
using PsychologicalSupportPlatform.Messaging.Domain.Entities;

namespace PsychologicalSupportPlatform.Messaging.Application.Mapping;

public class MessageMapper: IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<AddMessageDTO, Message>()
            .TwoWays()
            .Map(dest => dest, src => src);
    }
}

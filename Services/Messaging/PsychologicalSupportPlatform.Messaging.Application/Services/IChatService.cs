using PsychologicalSupportPlatform.Common;
using PsychologicalSupportPlatform.Messaging.Domain.DTOs;

namespace PsychologicalSupportPlatform.Messaging.Application.Services;

public interface IChatService
{
    Task<ResponseInfo> SendMessage(AddMessageDTO mesDTO);
}
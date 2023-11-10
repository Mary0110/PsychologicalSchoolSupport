using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using PsychologicalSupportPlatform.Messaging.Application.Services;

namespace PsychologicalSupportPlatform.Messaging.Infrastructure;

public class ChatHub : Hub
{
    public async Task SendMessage(int userId, string messageContent)
    {
        var chatService = Context.GetHttpContext().RequestServices.GetService<ChatService>();
        await chatService.SendMessage(userId, messageContent);
    }
}
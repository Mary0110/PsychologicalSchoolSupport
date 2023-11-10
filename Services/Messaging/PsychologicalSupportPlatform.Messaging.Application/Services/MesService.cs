using Microsoft.AspNetCore.SignalR;
using PsychologicalSupportPlatform.Messaging.Domain.Entities;

namespace PsychologicalSupportPlatform.Messaging.Application.Services;

public class ChatService
{
    private readonly IHubContext<ChatHub> hubContext;
    private readonly IMessageRepositoy messageRepository;

    public ChatService(IHubContext<ChatHub> hubContext,  IMessageRepositoy messageRepository)
    {
        this.hubContext = hubContext;
        this.messageRepository = messageRepository;
    }

    public async Task SendMessage(int userId, string messageContent)
    {
        var message = new Message
        {
            Text = messageContent,
            DateTime = DateTime.Now,
            ConsumerId = userId
        };

        messageRepository.AddMessage(message);
        await messageRepository.SaveChangesAsync();

        await hubContext.Clients.All.SendAsync("ReceiveMessage", message);
    }
}

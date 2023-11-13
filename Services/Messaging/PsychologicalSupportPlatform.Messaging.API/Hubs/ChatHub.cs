using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using PsychologicalSupportPlatform.Common;
using PsychologicalSupportPlatform.Messaging.API.Extensions;
using PsychologicalSupportPlatform.Messaging.Application.Services;
using PsychologicalSupportPlatform.Messaging.Domain.Entities;

namespace PsychologicalSupportPlatform.Messaging.API.Hubs;

public class ChatHub: Hub
{
    private readonly IChatService chatService;

    public ChatHub(IChatService chatService)
    {
        this.chatService = chatService;
    }

    [Authorize(Roles = Roles.Psychologist + "," + Roles.Student)]
    public async Task SendAsync(string message, string comsumerId)
    {
        var senderId = Context.User.GetLoggedInUserId<string>();
        var dateTime = DateTime.Now;
        await Clients.Group(comsumerId).SendAsync(
            "Receive message", message, senderId, dateTime
            );
        await chatService.AddMessageAsync(
            new Message(comsumerId, senderId, message, dateTime)
            );
    }
    
    [Authorize(Roles = Roles.Psychologist + "," + Roles.Student)]
    public override Task OnConnectedAsync()
    {
        var userId = Context.User.GetLoggedInUserId<string>();
        Groups.AddToGroupAsync(Context.ConnectionId, userId);

        return base.OnConnectedAsync();
    }
    
    [Authorize(Roles = Roles.Psychologist + "," + Roles.Student)]
    public async Task GetChatHistory(string otherUserId)
    {
        var chatHistory = await chatService.GetAllChatHistoryAsync(
            Context.User.GetLoggedInUserId<string>(), otherUserId
            );

        await Clients.Caller.SendAsync("Receive chat history", chatHistory);
    }
}

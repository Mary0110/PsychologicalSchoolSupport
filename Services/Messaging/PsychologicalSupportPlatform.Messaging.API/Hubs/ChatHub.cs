using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using PsychologicalSupportPlatform.Common;
using PsychologicalSupportPlatform.Messaging.API.Extensions;
using PsychologicalSupportPlatform.Messaging.Application.DTOs;
using PsychologicalSupportPlatform.Messaging.Application.Services;

namespace PsychologicalSupportPlatform.Messaging.API.Hubs;

public class ChatHub: Hub
{
    private readonly IChatService chatService;

    public ChatHub(IChatService chatService)
    {
        this.chatService = chatService;
    }

    [Authorize(Roles = Roles.Psychologist + "," + Roles.Student)]
    public async Task SendAsync(string message, string consumerId)
    {
        var senderId = Context.User.GetLoggedInUserId();
        var dateTime = DateTime.Now;
        
        await chatService.AddMessageAsync(
            new AddMessageDTO(senderId, consumerId, message, dateTime)
        );
        
        await Clients.Group(consumerId).SendAsync(
            "Receive message", message, senderId, dateTime
            );
    }
    
    [Authorize(Roles = Roles.Psychologist + "," + Roles.Student)]
    public override Task OnConnectedAsync()
    {
        var userId = Context.User.GetLoggedInUserId();
        Groups.AddToGroupAsync(Context.ConnectionId, userId);

        return base.OnConnectedAsync();
    }
    
    [Authorize(Roles = Roles.Psychologist + "," + Roles.Student)]
    public async Task GetChatHistory(string otherUserId)
    {
        var chatHistory = await chatService.GetAllChatHistoryAsync(
            Context.User.GetLoggedInUserId(), otherUserId
            );

        await Clients.Caller.SendAsync("Receive chat history", chatHistory);
    }
}

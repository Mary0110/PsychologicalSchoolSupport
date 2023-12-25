using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using PsychologicalSupportPlatform.Common;
using PsychologicalSupportPlatform.Messaging.API.Extensions;
using PsychologicalSupportPlatform.Messaging.Application.DTOs;
using PsychologicalSupportPlatform.Messaging.Application.Services;

namespace PsychologicalSupportPlatform.Messaging.API.Hubs;

public class ChatHub: Hub
{
    private readonly IChatService _chatService;

    public ChatHub(IChatService chatService)
    {
        _chatService = chatService;
    }

    [Authorize(Roles = Roles.Psychologist + "," + Roles.Student)]
    public async Task SendAsync(string consumerId, string message, CancellationToken token = default)
    {
        var senderId = Context.User.GetLoggedInUserId();
        var dateTime = DateTime.Now;
        
        await _chatService.AddMessageAsync(
            new AddMessageDTO(senderId, consumerId, message, dateTime), token
        );
        
        await Clients.Caller.SendAsync(
            method: "Receive message", arg1: message, arg2: senderId, arg3: dateTime
        );
        
        await Clients.Group(consumerId).SendAsync(
            method: "Receive message", arg1: message, arg2: senderId, arg3: dateTime
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
    public async Task GetChatHistoryAsync(string otherUserId, int pageNumber, int pageSize, CancellationToken token = default)
    {
        var chatHistory = await _chatService.GetAllChatHistoryAsync(
            new GetChatHistoryDTO(Context.User.GetLoggedInUserId(), otherUserId), 
            pageNumber, pageSize, 
            token
            );
        string jsonString = JsonConvert.SerializeObject(chatHistory);

        await Clients.Caller.SendAsync("Receive chat history", jsonString);
    }
}

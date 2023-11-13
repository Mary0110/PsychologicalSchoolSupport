using Microsoft.AspNetCore.Mvc;
using PsychologicalSupportPlatform.Messaging.Application.Services;

namespace PsychologicalSupportPlatform.Messaging.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatsController : ControllerBase
    {
        private readonly IChatService chatService;

        public ChatsController(IChatService chatService)
        {
            this.chatService = chatService;
        }
        
    }
}

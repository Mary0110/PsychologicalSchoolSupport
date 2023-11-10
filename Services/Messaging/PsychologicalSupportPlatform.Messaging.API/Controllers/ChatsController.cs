using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using PsychologicalSupportPlatform.Messaging.Application.Services;
using PsychologicalSupportPlatform.Messaging.Domain.DTOs;

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
        
        [HttpPost]
        // [Authorize(Roles = Roles.Student + "," + Roles.Admin)]
        public async Task<IActionResult> SendMessage(AddMessageDTO meetup)
        {
            var user = HttpContext.User.Identity as ClaimsIdentity;
            var curUserIdStr = user?.FindFirst(ClaimTypes.NameIdentifier).Value;
            bool success = int.TryParse(curUserIdStr, out int curUserId);

            if (!success)
            {
                return BadRequest();
            }
            var response = await chatService.SendMessage(meetup);
            
            if (!response.Success)
            {
                return NotFound(response.Message);
            }
            
            return Ok(response);
        }
    }
}

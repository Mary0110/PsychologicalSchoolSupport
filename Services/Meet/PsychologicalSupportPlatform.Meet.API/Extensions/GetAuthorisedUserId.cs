using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using PsychologicalSupportPlatform.Meet.API.Controllers;

namespace PsychologicalSupportPlatform.Meet.API.Extensions;

public static class GetAuthorisedUserId
{
    public static int GetCurrentUserId(this MeetupsController controller)
    {
        var userId = controller.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        int.TryParse(userId, out int curUserId);

        return curUserId;
    }
}

using System.Security.Claims;

namespace PsychologicalSupportPlatform.Messaging.API.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static string GetLoggedInUserId(this ClaimsPrincipal principal)
    {
        if (principal == null)
        {
            throw new ArgumentNullException(nameof(principal));
        }

        var loggedInUserId = principal.FindFirstValue(ClaimTypes.NameIdentifier);

        return loggedInUserId;
    }
}

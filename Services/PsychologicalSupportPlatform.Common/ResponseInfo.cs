using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace PsychologicalSupportPlatform.Common;

public class ResponseInfo
{
    protected HttpStatusCode Status { get; set; }
    
    public ResponseInfo(HttpStatusCode status)
    {
        Status = status;
    }
    
    public virtual IActionResult ToActionResult()
    {
        return Status switch
        {
            HttpStatusCode.OK => new OkResult(),
            HttpStatusCode.NoContent => new NoContentResult(),
            HttpStatusCode.NotFound => new NotFoundResult(),
            HttpStatusCode.Conflict => new ConflictResult(),
            _ => new BadRequestResult()
        };
    }
}

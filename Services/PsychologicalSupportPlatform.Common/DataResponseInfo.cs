using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace PsychologicalSupportPlatform.Common;

public class DataResponseInfo<T>: ResponseInfo
{
    private T? Data { get; set; }

    public DataResponseInfo(HttpStatusCode status, T data):base(status)
    {
        Status = status;
        Data = data;
    }
    
    public override IActionResult ToActionResult()
    {
        return Status switch
        {
            HttpStatusCode.OK => new OkObjectResult(Data),
            HttpStatusCode.NoContent => new NoContentResult(),
            HttpStatusCode.NotFound => new NotFoundResult(),
            HttpStatusCode.Conflict => new ConflictResult(),
            _ => new BadRequestResult()
        };
    }
}

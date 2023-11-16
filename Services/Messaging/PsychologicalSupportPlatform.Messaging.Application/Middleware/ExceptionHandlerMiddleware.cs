using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using PsychologicalSupportPlatform.Common;
using PsychologicalSupportPlatform.Common.Errors;

namespace PsychologicalSupportPlatform.Messaging.Application.Middleware;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate next;

    public ExceptionHandlerMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task InvokeAsync (HttpContext context) {
        
        try
        {
            await next(context);
        }
        catch (EntityNotFoundException ex)
        {
            var code = HttpStatusCode.BadRequest;
            await HandleExceptionAsync(context, code, ex);
        }
        catch (Exception ex)
        {
            var code = HttpStatusCode.InternalServerError;
            await HandleExceptionAsync(context, code, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, HttpStatusCode code, Exception ex)
    {
        var response = new ResponseInfo(success: false, message: ex.Message);
        var result = JsonSerializer.Serialize(response);

        var httpResponse = context.Response;
        httpResponse.ContentType = "application/json";
        httpResponse.StatusCode = (int)code;

        await httpResponse.WriteAsync(result);
    }
}
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PsychologicalSupportPlatform.Common;
using PsychologicalSupportPlatform.Common.Errors;

namespace PsychologicalSupportPlatform.Messaging.Application.Middleware;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlerMiddleware> _logger;

    public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync (HttpContext context) {

        try
        {
            await _next(context);
        }
        catch (EntityNotFoundException ex)
        {
            var code = HttpStatusCode.BadRequest;
            await HandleExceptionAsync(context, code, ex);
        }
        catch (Exception _) when (_ is OperationCanceledException or TaskCanceledException)
        {
            _logger.LogInformation( message: "Task cancelled");    
            
        }
        catch(Exception ex)
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
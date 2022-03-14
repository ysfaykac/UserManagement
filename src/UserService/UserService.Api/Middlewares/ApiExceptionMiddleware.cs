using System.Net;
using Microsoft.AspNetCore.Http.Features;
using Newtonsoft.Json;
using UserService.Domain.Exceptions;

namespace UserService.Api.Middlewares;

public class ApiExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ApiExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);
        }
        catch (Exception ex)
        {
            if (ex is ApiException)
            {
                await HandleExceptionAsync(context, ex);
            }
            else
            {
                throw;
            }

        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var response = context.Response;
        var customException = exception as BaseApiException;
        var statusCode = (int)HttpStatusCode.InternalServerError;
        var message = "Unexpected error";
        var description = "Unexpected error";

        if (null != customException)
        {
            message = customException.Message;
            description = customException.Description;
            statusCode = customException.Code;
        }

        response.ContentType = "application/json";
        response.StatusCode = statusCode;
        response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = description;
        await response.WriteAsync(JsonConvert.SerializeObject(new CustomErrorResponse
        {
            Message = message,
            Description = description
        }));
    }
}
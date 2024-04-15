namespace Syndicate.API.Middlewares;

using FluentValidation;
using Microsoft.AspNetCore.Http;
using Syndicate.Services;
using Syndicate.Services.Exceptions;
using System.Net;
using System.Text.Json;

public class ErrorHandlingMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var code = HttpStatusCode.InternalServerError;
        var message = "Something went wrong. Try a bit later.";
        //TODO: Logging

        if(exception is CustomValidationException)
        {
            code = HttpStatusCode.BadRequest;
            message = exception.Message;
        }

        return context.Response.WriteAsync(JsonSerializer.Serialize(ApiResponse<object>.Fail(
            code,
            message
        ), options: new() {  PropertyNamingPolicy = JsonNamingPolicy.CamelCase }));
    }
}
using System.Net;
using Amazon.S3;
using Domain.CustomExceptions;
using Microsoft.EntityFrameworkCore;

namespace Web.Api.Middleware;

public class CustomExceptionHandler : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (CategoryException exception)
        {
            context.Response.StatusCode = exception.StatusCode;
            await context.Response.WriteAsJsonAsync(new
            {
                Error = exception.Message
            });
        }
        catch (ImageException exception)
        {
            context.Response.StatusCode = (int)HttpStatusCode.Conflict;
            await context.Response.WriteAsJsonAsync(new
            {
                Error = exception.Message
            });
        }
        catch (DbUpdateConcurrencyException exception)
        {
            context.Response.StatusCode = (int)HttpStatusCode.Conflict;
            await context.Response.WriteAsJsonAsync(new
            {
                Error = "A concurrency conflict occurred.",
                Details = exception.Message
            });
        }
        catch (DbUpdateException exception)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            await context.Response.WriteAsJsonAsync(new
            {
                Error = "A database update error occurred.",
                Details = exception.InnerException?.Message ?? exception.Message
            });
        }
        catch (DeleteObjectsException  exception)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            await context.Response.WriteAsJsonAsync(new
            {
                Details = exception.InnerException?.Message ?? exception.Message
            });
        }
        catch (Exception exception)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsJsonAsync(new
            {
                Error = exception.Message
            });
        }
    }
}
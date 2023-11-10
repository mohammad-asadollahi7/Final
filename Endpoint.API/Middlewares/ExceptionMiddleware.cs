using Domain.Core.Exceptions;

namespace Endpoint.API.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }

        catch (AppException appEx)
        {
            context.Response.StatusCode = appEx.StatusCode;
            context.Response.WriteAsync(appEx.Message);
        }
        catch (Exception ex)
        {
            context.Response.WriteAsync(ex.Message);
        }


    }
}

public static class ExtensionMiddleware
{
    public static IApplicationBuilder UseExceptionMiddleware(this WebApplication builder)
    {
        return builder.UseMiddleware<ExceptionMiddleware>();
    }
}

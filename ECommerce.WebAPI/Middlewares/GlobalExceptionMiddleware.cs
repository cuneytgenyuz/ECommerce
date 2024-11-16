using System.Text.Json;

namespace ECommerce.WebAPI.Middlewares;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;

    public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Beklenmeyen bir hata oluştu.");
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var statusCode = StatusCodes.Status500InternalServerError;  
        object response = new
        {
            Code = "500",
            Message = "Bir hata oluştu. Lütfen tekrar deneyiniz.",
            Success = false,
            ErrorDetails = exception.Message 
        };

        if (exception is KeyNotFoundException)
        {
            statusCode = StatusCodes.Status404NotFound;
            response = new
            {
                Code = "404",
                Message = "Aradığınız kayıt bulunamadı.",
                Success = false
            };
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;

        return context.Response.WriteAsJsonAsync(response);
    }

}


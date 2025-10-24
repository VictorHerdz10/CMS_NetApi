// Presentation/Middleware/ExceptionMiddleware.cs
using System.Net;
using System.Text.Json;
using CMS_NetApi.Application.Exceptions;
using FluentValidation;
using Microsoft.Extensions.Logging; // <-- agrega

namespace CMS_NetApi.Presentation.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            // 1. **SIEMPRE** escribimos en consola
            _logger.LogError(ex, "Excepción no manejada: {Message}", ex.Message);

            // 2. Respondemos JSON con el error
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext ctx, Exception ex)
    {
        ctx.Response.ContentType = "application/json";

        int statusCode;
        object body;

        if (ex is HttpException httpEx)
        {
            statusCode = httpEx.StatusCode;
            body = new { statusCode, message = httpEx.Message };
        }
        else if (ex is ValidationException valEx)
        {
            statusCode = 400;
            body = new { statusCode, message = valEx.Errors.Select(e => e.ErrorMessage).ToArray() };
        }
        else
        {
            statusCode = 500;
            body = new { statusCode, message = $"Error interno: {ex.Message}" };
        }

        ctx.Response.StatusCode = statusCode;

        await JsonSerializer.SerializeAsync(ctx.Response.Body, body, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = false
        });
    }
}
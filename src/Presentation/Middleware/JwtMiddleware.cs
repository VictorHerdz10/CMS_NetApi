// JwtMiddleware.cs
using CMS_NetApi.Application.Services;
using System.Net;

namespace CMS_NetApi.Presentation.Middleware;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;

    public JwtMiddleware(RequestDelegate next) => _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        // ✅ Resuelves Scoped desde el contexto de la petición
        var jwt = context.RequestServices.GetRequiredService<IJwtService>();

        var token = ExtraerToken(context);
        if (token is not null)
        {
            var principal = jwt.ValidarToken(token);
            if (principal is null)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                await context.Response.WriteAsync("Token inválido o expirado");
                return;
            }
            context.User = principal;
        }

        await _next(context);
    }

    private static string? ExtraerToken(HttpContext ctx)
    {
        var auth = ctx.Request.Headers.Authorization.FirstOrDefault();
        if (string.IsNullOrEmpty(auth)) return null;
        var partes = auth.Split(' ', 2);
        return partes.Length == 2 && partes[0].Equals("Bearer", StringComparison.OrdinalIgnoreCase)
               ? partes[1]
               : null;
    }
}
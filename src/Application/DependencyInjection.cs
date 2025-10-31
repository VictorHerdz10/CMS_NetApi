using CMS_NetApi.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using MediatR;
using FluentValidation;
using CMS_NetApi.Application.Settings;
using Microsoft.Extensions.Configuration;
using CMS_NetApi.Application.Extensions;

namespace CMS_NetApi.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        // ✅ CONFIGURAR JWT Settings usando variables de entorno o appsettings
        services.Configure<JwtSettings>(options =>
        {
            options.SecretKey = configuration.GetValueOrEnv("JwtSettings:SecretKey", "JWT_SECRET_KEY");
            options.Issuer = configuration.GetValueOrEnv("JwtSettings:Issuer", "JWT_ISSUER");
            options.Audience = configuration.GetValueOrEnv("JwtSettings:Audience", "JWT_AUDIENCE");
            options.ExpirationInMinutes = int.Parse(
                configuration.GetValueOrEnv("JwtSettings:ExpirationInMinutes", "JWT_EXPIRATION_IN_MINUTES") ?? "60"
            );
        });
        // 1. MediatR + auto-discovery
        services.AddMediatR(Assembly.GetExecutingAssembly());

        // 2. FluentValidation
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        // 3. Custom pipeline (manual)
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IJwtService, JwtService>();
        return services;
    }
}
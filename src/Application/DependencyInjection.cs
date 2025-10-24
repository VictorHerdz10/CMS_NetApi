using CMS_NetApi.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using MediatR;
using FluentValidation;

namespace CMS_NetApi.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
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
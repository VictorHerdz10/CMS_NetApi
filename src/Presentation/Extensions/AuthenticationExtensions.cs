using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CMS_NetApi.Presentation.Extensions;

public static class AuthenticationExtensions
{
    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration cfg)
    {
        var key = Encoding.UTF8.GetBytes(cfg.GetValueOrEnv("JwtSettings:SecretKey", "JWT_SECRET_KEY"));
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opts =>
                {
                    opts.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = cfg.GetValueOrEnv("JwtSettings:Issuer", "JWT_ISSUER"),
                        ValidAudience = cfg.GetValueOrEnv("JwtSettings:Audience", "JWT_AUDIENCE"),
                        IssuerSigningKey = new SymmetricSecurityKey(key)
                    };
                });
        return services;
    }
}
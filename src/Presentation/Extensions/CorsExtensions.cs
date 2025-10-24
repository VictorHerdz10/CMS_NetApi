namespace CMS_NetApi.Presentation.Extensions;

public static class CorsExtensions
{
    public static IServiceCollection AddCorsPolicy(this IServiceCollection services, IConfiguration cfg)
    {
        var origins = cfg.GetValueOrEnv("AllowedOrigins", "ALLOWED_ORIGINS")?
                         .Split(',', StringSplitOptions.RemoveEmptyEntries) ?? Array.Empty<string>();

        services.AddCors(opts =>
            opts.AddDefaultPolicy(policy =>
                policy.WithOrigins(origins)
                      .AllowAnyMethod()
                      .AllowAnyHeader()));
        return services;
    }

    public static IApplicationBuilder UseCorsMiddleware(this IApplicationBuilder app)
    {
        app.UseCors();
        return app;
    }
}
using DotNetEnv;

namespace CMS_NetApi.Presentation.Extensions;

public static class EnvLoader
{
    public static void LoadEnv(this WebApplicationBuilder builder)
    {
        var envPath = Path.Combine(builder.Environment.ContentRootPath, ".env");
        if (File.Exists(envPath)) Env.Load(envPath);
    }
}
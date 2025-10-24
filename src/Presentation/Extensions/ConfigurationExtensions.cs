namespace CMS_NetApi.Presentation.Extensions;

public static class ConfigurationExtensions
{
    public static string GetValueOrEnv(this IConfiguration config, string jsonKey, string envKey) =>
        Environment.GetEnvironmentVariable(envKey) ?? config[jsonKey]!;
}
using Microsoft.Extensions.Configuration;

namespace CMS_NetApi.Infrastructure.Extensions;

internal static class ConfigurationExtensions
{
    public static string GetValueOrEnv(this IConfiguration config, string jsonKey, string envKey) =>
        Environment.GetEnvironmentVariable(envKey) ?? config[jsonKey]!;
}
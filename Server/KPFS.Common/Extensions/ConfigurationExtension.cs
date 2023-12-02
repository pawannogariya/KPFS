using Microsoft.Extensions.Configuration;

namespace KPFS.Common.Extensions
{
    public static class ConfigurationExtension
    {
        public static string GetJwtSecret(this IConfiguration configuration) => configuration.GetValue<string>("JWT:Secret");
        public static string GetJwtValidIssuer(this IConfiguration configuration) => configuration.GetValue<string>("JWT:ValidIssuer");
        public static string GetJwtValidAudience(this IConfiguration configuration) => configuration.GetValue<string>("JWT:ValidAudience");
    }
}

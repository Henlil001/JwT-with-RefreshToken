using JwT_with_RefreshToken.Common;

namespace JwT_with_RefreshToken.Configuration
{
    public static class ConfigValidator
    {
        public static AppSettings ValidateAppsettings(this IServiceCollection services, IConfiguration configuration)
        {
            var appSettings = configuration.GetSection("AppSettings").Get<AppSettings>();

            if (appSettings is null ||
                string.IsNullOrWhiteSpace(appSettings.DbConnString) ||
                string.IsNullOrWhiteSpace(appSettings.ClientDomain) ||
                string.IsNullOrWhiteSpace(appSettings.CorsPolicyName) ||
                string.IsNullOrWhiteSpace(appSettings.Issuer) ||
                string.IsNullOrWhiteSpace(appSettings.Audience) ||
                string.IsNullOrWhiteSpace(appSettings.TokenKey))
            {
                throw new Exception("AppSettings is not fully configured. Please check appsettings.json.");
            }
            return appSettings;
        }
    }
}

using JwT_with_RefreshToken.Common;

namespace JwT_with_RefreshToken.Configuration
{
    public static class ConfigValidator
    {
        public static void ValidateAppsettings(this IServiceCollection service, IConfiguration configuration)
        {
            var appSettings = configuration.GetSection("AppSettings").Get<AppSettings>();

            if (appSettings is null)
                throw new Exception("Missing AppSettings configuration section.");

            var missingFields = new List<string>();

            // Validera alla string-properties
            var stringProperties = typeof(AppSettings).GetProperties()
                .Where(p => p.PropertyType == typeof(string));

            foreach (var prop in stringProperties)
            {
                var value = prop.GetValue(appSettings) as string;
                if (string.IsNullOrWhiteSpace(value))
                {
                    missingFields.Add(prop.Name);
                }
            }

            // Validera alla List<string>-properties
            var listProperties = typeof(AppSettings).GetProperties()
                .Where(p => p.PropertyType == typeof(List<string>));

            foreach (var prop in listProperties)
            {
                var value = prop.GetValue(appSettings) as List<string>;
                if (value == null || value.Any(string.IsNullOrWhiteSpace))
                {
                    missingFields.Add(prop.Name);
                }
            }

            if (missingFields.Count != 0)
            {
                throw new Exception($"AppSettings is not fully configured. Missing or invalid: {string.Join(", ", missingFields)}");
            }
            else 
            {
                service.Configure<AppSettings>(configuration.GetSection("AppSettings"));

            }

        }
    }
}

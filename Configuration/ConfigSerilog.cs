using Serilog;
namespace JwT_with_RefreshToken.Configuration
{
    public static partial class ConfigSerilog
    {
        public static ILoggingBuilder ConfigurateSerilog(this ILoggingBuilder builder, IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration).
                CreateLogger();

            builder.ClearProviders()
                .AddSerilog(Log.Logger);
            builder.Services.AddSingleton(Log.Logger);

            return builder;
        }
    }
}

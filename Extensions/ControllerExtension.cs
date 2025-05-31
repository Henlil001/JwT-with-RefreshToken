using System.Text.Json.Serialization;

namespace JwT_with_RefreshToken.Extensions
{
    public static class ControllerExtension
    {
        public static IServiceCollection AddControllerExtension(this IServiceCollection services)
        {
            services.AddControllers()
                         .AddJsonOptions(options =>
                         {
                             options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                             options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                         });
            return services;
        }
    }
}

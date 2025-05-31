
using Azure.Identity;
using JwT_with_RefreshToken.Common;
using JwT_with_RefreshToken.Configuration;
using JwT_with_RefreshToken.Extensions;
using JwT_with_RefreshToken.Middleware;
using Microsoft.AspNetCore.Builder;
using Serilog;

namespace JwT_with_RefreshToken
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;

            if (builder.Environment.IsProduction())
            {
                builder.Configuration.AddAzureKeyVault(
                    new Uri($"https://{configuration["KeyVaultName"]}.vault.azure.net/"),
                    new DefaultAzureCredential());
            }

            var appSettings = builder.Services.ValidateAppsettings(configuration);

            builder.Logging.ConfigurateSerilog(configuration);

            builder.Services
                .AddScopedExtension()
                .AddControllerExtension()
                .AddCorsPolicyExtension(appSettings)
                .AddJwtBearerExtension(appSettings)
                .AddDbContextExtension(appSettings);

            builder.Services.AddOpenApi();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseCors(appSettings.CorsPolicyName);

            app.UseMiddleware<GlobalMiddleware>();

            app.MapControllers();

            try
            {
               await app.RunAsync();
            }
            finally 
            {
                await Log.CloseAndFlushAsync();
            }
        }
    }
}

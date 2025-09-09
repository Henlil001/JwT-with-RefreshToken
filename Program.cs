
using Azure.Identity;
using JwT_with_RefreshToken.Configuration;
using JwT_with_RefreshToken.DataAcces;
using JwT_with_RefreshToken.Extensions;
using JwT_with_RefreshToken.Middleware;
using JwT_with_RefreshToken.SeedData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
using Serilog;

namespace JwT_with_RefreshToken
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            if (builder.Environment.IsProduction())
            {
                builder.Configuration.AddAzureKeyVault(
                    new Uri($"https://{builder.Configuration["KeyVaultName"]}.vault.azure.net/"),
                    new DefaultAzureCredential());
            }

            builder.Services.ValidateAppsettings(builder.Configuration);

            builder.Logging.ConfigurateSerilog(builder.Configuration);

            builder.Services.AddControllerExtension();
            builder.Services.AddDbContextExtension(builder.Configuration);
            builder.Services.AddCorsPolicyExtension(builder.Configuration);
            builder.Services.AddJwtBearerExtension(builder.Configuration);
            builder.Services.AddScopedExtension();

            builder.Services.AddSwaggerExtended();

            var app = builder.Build();


            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerExtended();
            }

            app.UseHttpsRedirection();

            app.UseCors(builder.Configuration["AppSettings:CorsPolicyName"]!);

            app.UseMiddleware<GlobalMiddleware>();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            var roles = builder.Configuration.GetSection("AppSettings:Roles").Get<List<string>>();
            await app.InitializeRolesAsync(roles!);
            await app.AddProductsAsync();

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

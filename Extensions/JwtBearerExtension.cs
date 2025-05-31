using JwT_with_RefreshToken.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace JwT_with_RefreshToken.Extensions
{
    public static class JwtBearerExtension
    {
        public static IServiceCollection AddJwtBearerExtension(this IServiceCollection services, AppSettings appsettings)
        {
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
           .AddJwtBearer(opt =>
           {
               opt.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuer = true,
                   ValidateAudience = true,
                   ValidateLifetime = true,
                   ValidateIssuerSigningKey = true,
                   ValidIssuer = appsettings.Issuer,
                   ValidAudience = appsettings.Audience,
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appsettings.TokenKey)),
                   RequireExpirationTime = true,
               };
           });
            return services;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.Tokens;

namespace AlexBankExam.API.Services
{
    public static class ControllerServiceExtension
    {
        public static void ConfigureCors(this IServiceCollection services, IConfiguration config)
        {
            const string CORS_POLICY_NAME = "ApiCORSPolicy";
            //const string CORS_ORIGINS = "CorsOrigins";

            services.AddCors(options => {
                options.AddPolicy(name: CORS_POLICY_NAME, builder => {
                    builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .SetIsOriginAllowed(origin => true);
                });
            });
        }

        public static void ConfigureAuthenticationJwtBearer(this IServiceCollection services, IConfiguration Configuration)
        {
            var secret = "secreyKey123456";
            var key = Encoding.ASCII.GetBytes(secret);

            services.AddAuthentication(option => {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(option => {
                option.RequireHttpsMetadata = true;
                option.SaveToken = true;
                option.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateLifetime = true, // if true, validates the expiration of the token.                    
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidAudience = "https://localhost:44322",
                    ValidIssuer = "https://localhost:44322",
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
                option.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context => {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                            context.Response.Headers.Add("Token-Expired", "true");
                        return Task.CompletedTask;
                    }
                };
            })
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options => 
                Configuration.Bind("CookieSettings", options));
        }
    }
}

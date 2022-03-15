using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace GerenciadorCursos.ServicesExtensions
{
    public static class AuthenticationExtension
    {
        public static void AuthenticationService(
            this IServiceCollection services,
            IConfiguration config
        )
        {
            services
                .AddAuthentication(
                    options => {
                        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    }
                )
                .AddJwtBearer(
                    options => {
                        options.RequireHttpsMetadata = false;
                        options.SaveToken = true;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(
                                Encoding.ASCII.GetBytes(
                                    config.GetValue<string>("JwtToken:SecretKey")
                                )
                            ),
                            ValidateIssuer = false,
                            ValidateAudience = false
                        };
                    }
                );
        }
    }
}

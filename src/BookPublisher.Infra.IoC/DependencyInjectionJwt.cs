using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace BookPublisher.Infra.IoC;

public static class DependencyInjectionJwt
{
    public static void AddInfrastructureJwt(this IServiceCollection services, 
        IConfiguration configuration)
    {
        var JWTKey = Encoding.ASCII.GetBytes(configuration["Jwt:Key"]); 
        services.AddAuthentication(opt => 
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; 
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options => 
        {
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false, 
                ValidateIssuer = false, 
                ValidateLifetime = true, 
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(JWTKey), 
                ClockSkew = TimeSpan.Zero
            };
        });
    }
}

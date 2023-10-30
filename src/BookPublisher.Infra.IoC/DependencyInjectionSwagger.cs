using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace BookPublisher.Infra.IoC;

public static class DependencyInjectionSwagger
{
    public static void AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c => 
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "BookPublisherAPI",
                Version = "v1", 
                Description = "API para uma editora", 
                Contact = new OpenApiContact
                {
                    Name = "Matheus Siqueira", 
                    Email = "matheussiqueira.work@gmail.com", 
                    Url = new Uri("https://github.com/matheuz-siqueira")
                }
            });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization", 
                Type = SecuritySchemeType.ApiKey, 
                Scheme = "Bearer", 
                BearerFormat = "JWT", 
                In = ParameterLocation.Header, 
                Description = "Header de autorização JWT usando o esquema Bearer.\r\n\r\nInforme"
                + "'Bearer'[espaço] e o seu token.\r\n\r\nExemplo: Bearer NDczMjVjMDYtMWM5Yy00MDQ0LWE"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme, 
                            Id = "Bearer"
                        }
                    },
                    new string[]{}
                }
            });
        });
    }
}

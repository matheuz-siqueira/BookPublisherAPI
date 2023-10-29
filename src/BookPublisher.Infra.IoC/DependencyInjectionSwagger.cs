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
        });
    }
}

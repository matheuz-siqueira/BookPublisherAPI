using Microsoft.Extensions.DependencyInjection;

namespace BookPublisher.Infra.IoC;

public static class DependencyInjectionCORS
{
    public static void AddInfraCors(this IServiceCollection services)
    {
        services.AddCors(options => options.AddDefaultPolicy(builder => 
        {
            builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        }));
    }
}

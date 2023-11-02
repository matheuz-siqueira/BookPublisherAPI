using BookPublisher.Application.Dtos.Author;
using BookPublisher.Application.Dtos.Book;
using BookPublisher.Application.Dtos.User;
using BookPublisher.Application.Interfaces;
using BookPublisher.Application.Mappings;
using BookPublisher.Application.Services;
using BookPublisher.Application.Validation;
using BookPublisher.Domain.Interfaces;
using BookPublisher.Infra.Data.Context;
using BookPublisher.Infra.Data.Repositories;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookPublisher.Infra.IoC;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, 
    IConfiguration configuration)
    {
        AddContext(services, configuration);
        AddRepositories(services);
        AddServices(services);
        AddMapper(services);
        AddValidators(services); 
    }

    private static void AddContext(this IServiceCollection services, 
    IConfiguration configuration)
    {
         services.AddDbContext<AppDbContext>(options =>
            options.UseMySql(
                    configuration.GetConnectionString("DefaultConnection"),
                    ServerVersion.AutoDetect(configuration.GetConnectionString("DefaultConnection")),
                    b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)
                )
            );
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IAuthorRepository, AuthorRepository>(); 
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<IAuthorPerBookRepository, AuthorPerBookRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
    }

    private static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthorService, AuthorService>();
        services.AddScoped<IBookService, BookService>(); 
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddHttpContextAccessor();
        services.AddScoped<IUserLoggedService, UserLoggedService>();
        services.AddScoped<IFileService, FileService>();
    }

    private static void AddMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MappingProfile));
    }

    private static void AddValidators(this IServiceCollection services)
    {
        services.AddScoped<IValidator<RegisterAuthorRequestJson>, RegisterAuthorValidator>();
        services.AddScoped<IValidator<UpdateAuthorRequestJson>, UpdateAuthorValidator>();
        services.AddScoped<IValidator<RegisterBookRequestJson>, RegisterBookValidator>();
        services.AddScoped<IValidator<RegisterUserRequestJon>, RegisterUserValidator>();
        services.AddScoped<IValidator<AuthenticationRequestJson>, AuthenticationValidator>();
        services.AddScoped<IValidator<UpdatePasswordRequestJson>, UpdatePasswordValidator>();
    }
}

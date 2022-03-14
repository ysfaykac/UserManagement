using System.Reflection;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserService.Application.Services;
using UserService.Domain.Models;

namespace UserService.Application;

public static class ServiceRegistration
{
    public static IServiceCollection AddApplicationRegistration(this IServiceCollection services,IConfiguration configuration)
    {
        var assembly = Assembly.GetExecutingAssembly();

        services.AddAutoMapper(assembly);
        services.AddMediatR(assembly);
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IJwtGenerator, JwtGenerator>();
        services.Configure<TokenSettingOption>(configuration.GetSection("TokenSettingOption"));
        return services;
    }
}
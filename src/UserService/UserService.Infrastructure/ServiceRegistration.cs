using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserService.Application.Abstract;
using UserService.Infrastructure.Context;
using UserService.Infrastructure.Repositories;

namespace UserService.Infrastructure;

public static class ServiceRegistration
{
    public static IServiceCollection AddInfrastructureRegistration(this IServiceCollection services, IConfiguration configuration)
    {
        var conn = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<UserDbContext>(opt =>
        {
            opt.UseNpgsql(conn);
            opt.EnableSensitiveDataLogging();
        });
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserRoleRepository, UserRoleRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();

        var optionsBuilder = new DbContextOptionsBuilder<UserDbContext>().UseNpgsql(conn);
        using var dbContext = new UserDbContext(optionsBuilder.Options,null);
        dbContext.Database.EnsureCreated();
        dbContext.Database.Migrate();
        return services;
    }
}
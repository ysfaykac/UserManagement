using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserManagement.Application.Abstract;
using UserManagement.Infrastructure.Context;
using UserManagement.Infrastructure.Repositories;

namespace UserManagement.Infrastructure;

public static class ServiceRegistration
{
    public static IServiceCollection AddInfrastructureRegistration(this IServiceCollection services, IConfiguration configuration)
    {
        var conn = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<UserManagementDbContext>(opt =>
        {
            opt.UseNpgsql(conn);
            opt.EnableSensitiveDataLogging();
        });
        services.AddScoped<IUserInfoRepository, UserInfoRepository>();

        var optionsBuilder = new DbContextOptionsBuilder<UserManagementDbContext>().UseNpgsql(conn);
        using var dbContext = new UserManagementDbContext(optionsBuilder.Options,null);
        dbContext.Database.EnsureCreated();
        dbContext.Database.Migrate();
        return services;
    }
}
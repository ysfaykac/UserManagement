using EventBus.Base.Abstraction;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Polly;
using UserManagement.Application.IntegrationEvents;
using UserManagement.GrpcService.IntegrationEvents;
using UserManagement.Infrastructure.Context;

namespace UserManagement.GrpcService.Extensions;

public static class WebAppExtensions
{
    public static void MigrateDbContext(this WebApplication webApplication
    ) 
    {
        using (var scope = webApplication.Services.CreateScope())
        {
            var serviceProvider = scope.ServiceProvider;
            var logger = serviceProvider.GetRequiredService<ILogger<UserManagementDbContext>>();
            var context = serviceProvider.GetRequiredService<UserManagementDbContext>();
            try
            {
                logger.LogInformation("Migration Database associated with context {DbContext}", typeof(UserManagementDbContext).Name);
                var retry = Policy.Handle<SqlException>().WaitAndRetry(new List<TimeSpan>()
                {
                    TimeSpan.FromSeconds(3),
                    TimeSpan.FromSeconds(5),
                    TimeSpan.FromSeconds(8)
                });
                retry.Execute(() => CreateAndMigrate(context, serviceProvider));
                logger.LogInformation("Database migrated");
            }
            catch (Exception e)
            {

                logger.LogError("an error occured while migration the database used on context {DbContext}", typeof(UserManagementDbContext).Name);
            }
        }

    }

    public static void ConfigureSubscription(this IServiceProvider serviceProvider)
    {
        var eventBus = serviceProvider.GetRequiredService<IEventBus>();

        eventBus.Subscribe<UserRegisteredIntegrationEvent, UserRegisteredIntegrationEventHandler>();
        eventBus.Subscribe<UserUpdatedIntegrationEvent, UserUpdatedIntegrationEventHandler>();
    }
    private static void CreateAndMigrate(UserManagementDbContext context, IServiceProvider sp)
    {
        context.Database.EnsureCreated();
        context.Database.Migrate();
        var dbContextSeeder = new UserManagementDbContextSeed();
        dbContextSeeder.SeedAsync(context, sp).Wait();
    }
}
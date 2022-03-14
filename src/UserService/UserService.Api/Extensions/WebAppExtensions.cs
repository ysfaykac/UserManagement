using EventBus.Base.Abstraction;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Polly;
using UserService.Api.IntegrationEvents;
using UserService.Domain.AggregateModels;
using UserService.Infrastructure.Context;

namespace UserService.Api.Extensions;

public static class WebAppExtensions
{
    public static void MigrateDbContext(this WebApplication webApplication
    ) 
    {
        using (var scope = webApplication.Services.CreateScope())
        {
            var serviceProvider = scope.ServiceProvider;
            var logger = serviceProvider.GetRequiredService<ILogger<UserDbContext>>();
            var context = serviceProvider.GetRequiredService<UserDbContext>();
            try
            {
                logger.LogInformation("Migration Database associated with context {DbContext}", typeof(UserDbContext).Name);
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

                logger.LogError("an error occured while migration the database used on context {DbContext}", typeof(UserDbContext).Name);
            }
        }

    }

    public static void ConfigureSubscription(this IServiceProvider serviceProvider)
    {
        var eventBus = serviceProvider.GetRequiredService<IEventBus>();

        eventBus.Subscribe<UserRegisteredApprovedIntegrationEvent, UserRegisteredApprovedIntegrationEventHandler>();
        eventBus.Subscribe<UserRegisteredDeclinedIntegrationEvent, UserRegisteredDeclinedIntegrationEventHandler>();
        eventBus.Subscribe<UserEnabledStatusUpdateIntegrationEvent, UserEnabledStatusUpdateIntegrationEventHandler>();
    }
    private static void CreateAndMigrate(UserDbContext context, IServiceProvider sp)
    {
        context.Database.EnsureCreated();
        context.Database.Migrate();
        var dbContextSeeder = new UserDbContextSeed();
        dbContextSeeder.SeedAsync(context, sp).Wait();
    }
}
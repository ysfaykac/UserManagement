using EventBus.Base;
using EventBus.Factory;
using UserService.Api.IntegrationEvents;

namespace UserService.Api.Extensions;

public static class ServiceCollectionExtensions
{
    static readonly string CorsName = "UserServiceWebCors";

    public static IServiceCollection AddEventBus(this IServiceCollection services)
    {
        services.AddSingleton(sp =>
        {
            EventBusConfig config = new()
            {
                ConnectionRetryCount = 5,
                EventNameSuffix = "IntegrationEvent",
                SubscriberClientAppName = "UserService",
                EventBusType = EventBusType.RabbitMQ
            };
            return EventBusFactory.Create(config, sp);
        });
        return services;
    }

    public static IServiceCollection AddCorsService(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(CorsName, builder =>
            {
                builder.WithOrigins("http://localhost:54555").AllowAnyHeader().AllowAnyMethod();
            });
        });
        return services;
    }

    public static IServiceCollection AddEventHandlers(this IServiceCollection services)
    {
        services.AddTransient<UserRegisteredApprovedIntegrationEventHandler>();
        services.AddTransient<UserRegisteredDeclinedIntegrationEventHandler>();
        services.AddTransient<UserEnabledStatusUpdateIntegrationEventHandler>();
        return services;
    }
}
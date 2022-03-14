using MediatR;
using UserService.Domain.Abstract;
using UserService.Infrastructure.Context;

namespace UserService.Infrastructure.Extensions;

public static class MediatorExtension
{
    public static async Task DispatchDomainEventsAsync(this IMediator mediator, UserDbContext ctx)
    {
        var domainEntities = ctx.ChangeTracker
            .Entries<BaseEntity>()
            .Where(x =>  x.Entity.DomainEvents!=null && x.Entity.DomainEvents.Any()).ToList();
        if (domainEntities is not {})
        {
            if (domainEntities != null)
            {
                var domainEvents = domainEntities.SelectMany(x => x.Entity.DomainEvents).ToList();

                domainEntities.ToList().ForEach(e => e.Entity.ClearDomainEvents());

                foreach (var domainEvent in domainEvents)
                    await mediator.Publish(domainEvent);
            }
        }
       
    }
}
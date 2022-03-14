using MediatR;
using UserManagement.Domain.Abstract;
using UserManagement.Infrastructure.Context;

namespace UserManagement.Infrastructure.Extensions;

public static class MediatorExtension
{
    public static async Task DispatchDomainEventsAsync(this IMediator mediator, UserManagementDbContext ctx)
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
using MediatR;

namespace UserManagement.Domain.Abstract;

public abstract class BaseEntity: ISoftDeletedEntity
{
    public BaseEntity()
    {
        Id=Guid.NewGuid();
        CreateDate=DateTime.Now;
        IsDeleted=false;
    }
    public  Guid Id { get; set; }
    public DateTime CreateDate { get; set; }

    private List<INotification> domainEvents = new();
    public IReadOnlyCollection<INotification> DomainEvents => domainEvents.AsReadOnly();
    public void AddDomainEvent(INotification eventItem)
    {
        domainEvents.Add(eventItem);
    }

    public void RemoveDomainEvent(INotification eventItem)
    {
        domainEvents?.Remove(eventItem);
    }

    public void ClearDomainEvents()
    {
        domainEvents?.Clear();
    }

    public bool IsDeleted { get; set; }
}
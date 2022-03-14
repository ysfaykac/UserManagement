using UserService.Domain.Abstract;

namespace UserService.Domain.AggregateModels;

public class UserRole:BaseEntity,IAggregateRoot
{
  
    public UserRole(Guid userId, Guid roleId):base()
    {
        UserId = userId;
        RoleId = roleId;
    }
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }
    public virtual Role Role { get; set; }
}
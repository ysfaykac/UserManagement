using UserManagement.Domain.Abstract;

namespace UserManagement.Domain.AggregateModels;

public class UserRole:BaseEntity,IAggregateRoot
{
  
    public UserRole(Guid userId, Guid roleId):base()
    {
        UserId = userId;
        RoleId = roleId;
    }
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }
}
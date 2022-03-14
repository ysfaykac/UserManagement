using UserManagement.Domain.Abstract;

namespace UserManagement.Domain.AggregateModels;

public class UserInfo : BaseEntity, IAggregateRoot
{
    public UserInfo()
    {
     
    }
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public bool IsEnabled { get; set; }

    public int userStatusId { get; private set; }
    public UserStatus UserStatus { get;  set; }
    public UserInfo(Guid id,string userName, string firstName, string lastName, string email, UserStatus userStatus) : this()
    {
        Id = id;
        UserName = userName;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        userStatusId = userStatus.Id;
    }
    
    public void SetUserStatusId(UserStatus userStatus)
    {
        userStatusId = userStatus.Id;
    }
    
   
}
using UserService.Domain.Abstract;

namespace UserService.Domain.AggregateModels;

public class User : BaseEntity, IAggregateRoot
{
    public User()
    {
        Id = Guid.NewGuid();
        CreateDate = DateTime.Now;
        IsDeleted = false;
        _userRoles = new List<UserRole>();
    }
    public string UserName { get; set; }
    public string Password { get; set; }
    public bool IsEnabled { get; set; }

    private int userStatusId;
    public UserStatus UserStatus { get; private set; }

    private readonly List<UserRole> _userRoles;

    public IReadOnlyCollection<UserRole> UserRoles => _userRoles;
    public User(string userName, string password, UserStatus userStatus) : this()
    {
        UserName = userName;
        Password = password;
        userStatusId = userStatus.Id;
    }

    public void SetStatusId(UserStatus userStatus)
    {
        userStatusId = userStatus.Id;
    }
    public void AddRole(Guid roleId)
    {
        if (_userRoles.All(t => t.RoleId != roleId))
        {
            var userRole = new UserRole(Id, roleId);
            _userRoles.Add(userRole);
        }
    }
}
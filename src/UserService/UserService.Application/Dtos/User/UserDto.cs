using UserService.Application.Dtos.UserRole;

namespace UserService.Application.Dtos.User;

public class UserDto
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string UserStatus { get; set; }
    public List<UserRoleDto>  UserRoles { get; set; }

}
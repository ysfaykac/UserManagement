namespace UserService.Application.Dtos.UserRole;

public class UserRoleDto
{
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }
    public string RoleName { get; set; }
}
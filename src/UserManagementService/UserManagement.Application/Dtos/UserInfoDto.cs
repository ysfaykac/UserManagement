using UserManagement.Domain.AggregateModels;

namespace UserManagement.Application.Dtos;

public class UserInfoDto
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public bool IsEnabled { get; set; }
    public UserStatusDto UserStatus { get; set; }

}
using UserManagement.Domain.Abstract;

namespace UserManagement.Application.Dtos;

public class UserStatusDto:Enumeration
{
    public static UserStatusDto Pending = new(1, nameof(Pending).ToLowerInvariant());
    public static UserStatusDto Approved = new(2, nameof(Approved).ToLowerInvariant());
    public static UserStatusDto Declined = new(3, nameof(Declined).ToLowerInvariant());
    public static UserStatusDto Blacklist = new(4, nameof(Blacklist).ToLowerInvariant());
    public UserStatusDto(int id, string name) : base(id, name)
    {
    }
}
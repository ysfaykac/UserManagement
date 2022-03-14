using UserManagement.Domain.Abstract;

namespace UserManagement.Domain.AggregateModels;

public class UserStatus: Enumeration
{
    public static UserStatus Pending = new(1, nameof(Pending).ToLowerInvariant());
    public static UserStatus Approved = new(2, nameof(Approved).ToLowerInvariant());
    public static UserStatus Declined = new(3, nameof(Declined).ToLowerInvariant());
    public static UserStatus Blacklist = new(4, nameof(Blacklist).ToLowerInvariant());
  
    public UserStatus(int id, string name) : base(id, name)
    {
    }

    public static IEnumerable<UserStatus> List() =>
        new[] { Pending, Approved, Declined, Blacklist };

    public static UserStatus FromName(string name)
    {
        var state = List().SingleOrDefault(x => String.Equals(x.Name, name, StringComparison.CurrentCultureIgnoreCase));

        return state ?? throw new Exception($"Possible values for user status: {String.Join(",", List().Select(x => x.Name))}");
    }

    public static UserStatus From(int id)
    {
        var state = List().SingleOrDefault(p => p.Id == id);

        return state ?? throw new Exception($"Possible values for user status: {String.Join(",", List().Select(x => x.Name))}");
    }
}
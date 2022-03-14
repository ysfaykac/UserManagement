namespace UserManagement.Domain.Models;

public class AuthenticationTicketInfo
{
    public string AccessToken { get; set; }
    public DateTime Expires { get; set; }
}
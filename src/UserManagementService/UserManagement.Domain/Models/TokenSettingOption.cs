namespace UserManagement.Domain.Models;

public class TokenSettingOption
{
    public string Audience { get; set; }
    public string Issuer { get; set; }
    public long AccessTokenExpiration { get; set; }
    public string PrivateKey { get; set; }
}
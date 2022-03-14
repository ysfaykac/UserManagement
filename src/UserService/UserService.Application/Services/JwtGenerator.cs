using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using UserService.Application.Dtos.User;
using UserService.Domain.AggregateModels;
using UserService.Domain.Models;

namespace UserService.Application.Services;

public class JwtGenerator:IJwtGenerator
{
    private TokenSettingOption _tokenSettingOption;
    public JwtGenerator(IOptions<TokenSettingOption> options)
    {
        _tokenSettingOption = options.Value;
    }

    private DateTime _accessTokenExpiration;

    public AuthenticationTicketInfo CreateToken(UserDto user)
    {
        AuthenticationTicketInfo authenticationTicketInfo= new AuthenticationTicketInfo();
           var jwt = CreateJwtSecurityToken(user);
        var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        var token = jwtSecurityTokenHandler.WriteToken(jwt);
        authenticationTicketInfo.AccessToken = token;
        authenticationTicketInfo.Expires=_accessTokenExpiration;
        return authenticationTicketInfo;
    }

    public AuthenticationTicketInfo RefreshToken(AuthenticationTicketInfo token)
    {
        throw new NotImplementedException();
    }

    private JwtSecurityToken CreateJwtSecurityToken(UserDto user)
    {
        _accessTokenExpiration = DateTime.Now.AddMinutes(Convert.ToDouble(_tokenSettingOption.AccessTokenExpiration));
        IEnumerable<Claim> claims = GetJWTClaims(user);
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenSettingOption.PrivateKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var jwt = new JwtSecurityToken(
            issuer: _tokenSettingOption.Issuer,
            audience: _tokenSettingOption.Audience,
            expires: _accessTokenExpiration,
            notBefore: DateTime.Now,
            claims: claims,
            signingCredentials: creds
        );
        return jwt;
    }

    private IEnumerable<Claim> GetJWTClaims(UserDto userDto)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, userDto.Id.ToString()),
            new Claim(ClaimTypes.Email, userDto.UserName ?? string.Empty),
            new Claim(ClaimTypes.Name, (userDto.UserName) ?? string.Empty),
            new Claim(ClaimTypes.Actor, userDto.UserStatus ?? string.Empty),
        };
        userDto.UserRoles.ForEach(role => claims.Add(new Claim(ClaimTypes.Role, role.RoleName ?? string.Empty)));

        return claims;
    }
}
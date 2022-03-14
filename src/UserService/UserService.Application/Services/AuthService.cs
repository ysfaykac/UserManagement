using System.Net;
using AutoMapper;
using UserService.Application.Abstract;
using UserService.Application.Dtos.User;
using UserService.Application.Encryption;
using UserService.Domain.AggregateModels;
using UserService.Domain.Exceptions;
using UserService.Domain.Models;

namespace UserService.Application.Services;

public class AuthService:IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtGenerator _jwtGenerator;
    private readonly IMapper _mapper;
    public AuthService(IUserRepository userRepository, IJwtGenerator jwtGenerator, IMapper mapper)
    {
        _userRepository = userRepository;
        _jwtGenerator = jwtGenerator;
        _mapper = mapper;
    }
    public async Task<AuthenticationTicketInfo> GetAuthentication(string userName, string password)
    {
        var user =await _userRepository.GetSingleAsync(t=>t.UserName==userName);
        if (user == null)
            throw new ApiException(HttpStatusCode.NotFound, "Kullanıcı adınız veya şifreniz yanlıştır.");
        if (user.UserStatus.Equals(UserStatus.Pending))
            throw new ApiException(HttpStatusCode.Forbidden, "Kullanıcı durumunuz henüz onaylanmamıştır.");
        if (user.UserStatus.Equals(UserStatus.Declined) || user.UserStatus.Equals(UserStatus.Blacklist))
            throw new ApiException(HttpStatusCode.Forbidden, "Kullanıcınız silinmiştir.");
        var hashPassword = EncryptionHelper.Encrypt(password, userName);
        if (!user.Password.Equals(hashPassword))
        {
            throw new ApiException(HttpStatusCode.NotFound, "Kullanıcı adınız veya şifreniz yanlıştır.");
        }
        var userDto = _mapper.Map<UserDto>(user);
        return _jwtGenerator.CreateToken(userDto);
    }
}
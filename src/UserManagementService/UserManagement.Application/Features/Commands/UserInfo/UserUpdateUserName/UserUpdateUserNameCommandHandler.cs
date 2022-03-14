using MediatR;
using UserManagement.Application.Abstract;
using UserManagement.Domain.Models;

namespace UserManagement.Application.Features.Commands.UserInfo.UserUpdateUserName;

public class UserUpdateUserNameCommandHandler : IRequestHandler<UserUpdateUserNameCommand, bool>
{
    private readonly IUserInfoRepository _userInfoRepository;

    public UserUpdateUserNameCommandHandler(IUserInfoRepository userInfoRepository)
    {
        _userInfoRepository = userInfoRepository;
      
    }

    public async Task<bool> Handle(UserUpdateUserNameCommand request, CancellationToken cancellationToken)
    {
        var user = await _userInfoRepository.GetById(request.UserId);
        if (user == null)
        {
            return false;
        }

        user.UserName = request.UserName;
        _userInfoRepository.Update(user);
        var response = await _userInfoRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return response > 0;
    }
}
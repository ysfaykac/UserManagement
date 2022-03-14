using MediatR;
using UserService.Application.Abstract;
using UserService.Domain.AggregateModels;

namespace UserService.Application.Features.Commands.User;

public class UserStatusChangeCommandHandler:IRequestHandler<UserStatusChangeCommand,bool>
{
    private readonly IUserRepository _userRepository;

    public UserStatusChangeCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<bool> Handle(UserStatusChangeCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetSingleAsync(t => t.Id == request.UserId);
        if (user==null)
        {
            return false;
        }
        user.SetStatusId(request.UserStatus);
        _userRepository.Update(user);
        var response = await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return response>0;
    }
}
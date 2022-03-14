using MediatR;
using UserService.Application.Abstract;

namespace UserService.Application.Features.Commands.User;

public class UserEnabledStatusUpdateCommandHandler:IRequestHandler<UserEnabledStatusUpdateCommand,bool>
{
    private readonly IUserRepository _userRepository;

    public UserEnabledStatusUpdateCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<bool> Handle(UserEnabledStatusUpdateCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetSingleAsync(t => t.Id == request.Id);
        if (user == null)
        {
            return false;
        }
        user.IsEnabled = request.IsEnabled;
        _userRepository.Update(user);
        var response = await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return response > 0;
    }
}
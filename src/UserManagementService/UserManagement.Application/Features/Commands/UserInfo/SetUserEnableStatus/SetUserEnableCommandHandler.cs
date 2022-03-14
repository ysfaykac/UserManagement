using EventBus.Base.Abstraction;
using MediatR;
using UserManagement.Application.Abstract;
using UserManagement.Application.IntegrationEvents;

namespace UserManagement.Application.Features.Commands.UserInfo.SetUserEnableStatus;

public class SetUserEnableCommandHandler:IRequestHandler<SetUserEnableCommand,bool>
{
    private readonly IUserInfoRepository _userInfoRepository;
    private IEventBus _eventBus;

    public SetUserEnableCommandHandler(IUserInfoRepository userInfoRepository, IEventBus eventBus)
    {
        _userInfoRepository = userInfoRepository;
        _eventBus = eventBus;
    }
    public async Task<bool> Handle(SetUserEnableCommand request, CancellationToken cancellationToken)
    {
        var user = await _userInfoRepository.GetById(request.UserId);
        if (user == null)
        {
            return false;
        }
        user.IsEnabled = request.IsEnabled;
        _userInfoRepository.Update(user);
        var response = await _userInfoRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        if (response > 0)
        {
            _eventBus.Publish(new UserEnabledStatusUpdateIntegrationEvent(request.UserId,request.IsEnabled));
        }
        return response > 0;
    }
}
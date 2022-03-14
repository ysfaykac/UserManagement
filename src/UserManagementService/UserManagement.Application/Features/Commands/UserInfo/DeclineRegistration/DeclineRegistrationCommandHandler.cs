using EventBus.Base.Abstraction;
using MediatR;
using UserManagement.Application.Abstract;
using UserManagement.Application.Constants;
using UserManagement.Application.IntegrationEvents;
using UserManagement.Domain.AggregateModels;
using UserManagement.Domain.Models;

namespace UserManagement.Application.Features.Commands.UserInfo.DeclineRegistration;

public class DeclineRegistrationCommandHandler : IRequestHandler<DeclineRegistrationCommand,ServiceResult<string>>
{
    private readonly IUserInfoRepository _userInfoRepository;
    private IEventBus _eventBus;
    public DeclineRegistrationCommandHandler(IUserInfoRepository userInfoRepository, IEventBus eventBus)
    {
        _userInfoRepository = userInfoRepository;
        _eventBus = eventBus;
    }
    public async Task<ServiceResult<string>> Handle(DeclineRegistrationCommand request, CancellationToken cancellationToken)
    {
        ServiceResult<string> result = new ServiceResult<string>();
;        var userInfo =await _userInfoRepository.GetSingleAsync(t => t.Id == request.UserId);
        if (userInfo!=null)
        {
            if (userInfo.userStatusId==UserStatus.Declined.Id)
            {
                result.IsSucceeded = false;
                result.Result = BaseMessages.DeclineRegistrationMessages.Succeess;
                return result;
            }

            if (userInfo.userStatusId!=UserStatus.Approved.Id) return result;
            userInfo.SetUserStatusId(UserStatus.Declined);
            _userInfoRepository.Update(userInfo);
            var response = await _userInfoRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            if (response>0)
            {
                result.Result = BaseMessages.DeclineRegistrationMessages.Succeess;
                result.IsSucceeded= true;
                _eventBus.Publish(new UserRegisteredDeclinedIntegrationEvent(userInfo.Id));
                return result;
                // kullanıcıya mail gönderimi
            }
            result.Result = BaseMessages.DeclineRegistrationMessages.Fail;
            result.IsSucceeded = false;
            return result;
        }
        result.Result = BaseMessages.DeclineRegistrationMessages.UserNotFount;
        result.IsSucceeded = false;
        return result;
    }
}
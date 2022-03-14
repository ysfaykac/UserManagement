using EventBus.Base.Abstraction;
using MediatR;
using UserManagement.Application.Abstract;
using UserManagement.Application.Constants;
using UserManagement.Application.IntegrationEvents;
using UserManagement.Domain.AggregateModels;
using UserManagement.Domain.Models;

namespace UserManagement.Application.Features.Commands.UserInfo.AcceptRegistration;

public class AcceptRegistrationCommandHandler:IRequestHandler<AcceptRegistrationCommand,ServiceResult<string>>
{
    private readonly IUserInfoRepository _userInfoRepository;
    private IEventBus _eventBus;
    public AcceptRegistrationCommandHandler(IUserInfoRepository userInfoRepository, IEventBus eventBus)
    {
        _userInfoRepository = userInfoRepository;
        _eventBus = eventBus;
    }
    public async Task<ServiceResult<string>> Handle(AcceptRegistrationCommand request, CancellationToken cancellationToken)
    {
        ServiceResult<string> result = new ServiceResult<string>();
;        var userInfo =await _userInfoRepository.GetSingleAsync(t => t.Id == request.UserId);
        if (userInfo!=null)
        {
            if (userInfo.userStatusId==UserStatus.Approved.Id)
            {
                result.IsSucceeded = false;
                result.Result = BaseMessages.AcceptRegistrationMessages.AcceptRegistrationFail;
                return result;
            }
            if (userInfo.userStatusId!=UserStatus.Pending.Id) return result;
            userInfo.SetUserStatusId(UserStatus.Approved);
            _userInfoRepository.Update(userInfo);
            var response = await _userInfoRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            if (response>0)
            {
                result.Result = BaseMessages.AcceptRegistrationMessages.AcceptRegistrationSucceess;
                result.IsSucceeded= true;
                _eventBus.Publish(new UserRegisteredApprovedIntegrationEvent(userInfo.Id));
                return result;
                // kullanıcıya mail gönderimi
            }
            result.Result = BaseMessages.AcceptRegistrationMessages.AcceptRegistrationFail;
            result.IsSucceeded = true;
            return result;
        }
        result.Result = BaseMessages.AcceptRegistrationMessages.UserNotFount;
        result.IsSucceeded = true;
        return result;
    }
}
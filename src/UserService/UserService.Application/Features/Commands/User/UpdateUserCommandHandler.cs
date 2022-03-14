using EventBus.Base.Abstraction;
using MediatR;
using UserService.Application.Abstract;
using UserService.Application.Constants;
using UserService.Application.Encryption;
using UserService.Application.IntegrationEvents;
using UserService.Domain.Models;

namespace UserService.Application.Features.Commands.User;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, ServiceResult<string>>
{
    private readonly IUserRepository _userRepository;
    private readonly IEventBus _eventBus;

    public UpdateUserCommandHandler(IUserRepository userRepository, IEventBus eventBus)
    {
        _userRepository = userRepository;
        _eventBus = eventBus;
    }

    public async Task<ServiceResult<string>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        ServiceResult<string> result = new ServiceResult<string>();
        var user = await _userRepository.GetById(request.Id);
        if (user == null)
        {
            result.Result = BaseMessages.UserUpdateMessages.UserNotFount;
            result.IsSucceeded = false;
            return result;
        }
        var hashPassword = EncryptionHelper.Encrypt(request.Password, request.UserName);
        user.Password = hashPassword;
        user.UserName = request.UserName;
        _userRepository.Update(user);
        var response = await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        if (response > 0)
        {
            _eventBus.Publish(new UserUpdatedIntegrationEvent(request.Id,request.UserName));
            result.Result = BaseMessages.UserUpdateMessages.UserUpdateSucceess;
            result.IsSucceeded = true;
            return result;
        }

        result.Result = BaseMessages.UserUpdateMessages.UserUpdateFail;
        result.IsSucceeded = true;
        return result;
    }
}
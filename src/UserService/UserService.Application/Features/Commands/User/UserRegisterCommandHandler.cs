using System.Net;
using EventBus.Base.Abstraction;
using MediatR;
using UserService.Application.Abstract;
using UserService.Application.Constants;
using UserService.Application.Encryption;
using UserService.Application.IntegrationEvents;
using UserService.Domain.AggregateModels;
using UserService.Domain.Exceptions;
using UserService.Domain.Models;

namespace UserService.Application.Features.Commands.User;

public class UserRegisterCommandHandler : IRequestHandler<UserRegisterCommand, ServiceResult<string>>
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IEventBus _eventBus;
    public UserRegisterCommandHandler(IUserRepository userRepository, IRoleRepository roleRepository, IEventBus eventBus)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _eventBus = eventBus;
    }
    public  async Task<ServiceResult<string>> Handle(UserRegisterCommand request, CancellationToken cancellationToken)
    {
        ServiceResult<string> result = new ServiceResult<string>();
        try
        {
            var hashPassword = EncryptionHelper.Encrypt(request.Password, request.Username);
            var user = new Domain.AggregateModels.User(request.Username, hashPassword, UserStatus.Pending);
            var role =await _roleRepository.GetSingleAsync(t => t.Name == "User");
            user.AddRole(role.Id);
            var data = await _userRepository.AddAsync(user);
            var response = await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            if (response>0)
            {
                var userRegisterEvent =
                    new UserRegisteredIntegrationEvent(data.Id, data.UserName, request.FirstName, request.LastName, request.Email);
                _eventBus.Publish(userRegisterEvent);
                result.Result = BaseMessages.UserCreateMessages.UserCreateSucceess;
                result.IsSucceeded = true;
            }
            else
            {
                result.Result = BaseMessages.UserCreateMessages.UserCreateFail;
                result.IsSucceeded = false;
            }

        }
        catch (Exception e)
        {
            throw new ApiException(HttpStatusCode.InternalServerError, BaseMessages.UserCreateMessages.UserCreateFail);
        }

        return result;


    }
}
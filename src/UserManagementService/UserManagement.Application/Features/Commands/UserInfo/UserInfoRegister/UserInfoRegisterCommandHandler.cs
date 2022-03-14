using System.Net;
using MediatR;
using UserManagement.Application.Abstract;
using UserManagement.Application.Constants;
using UserManagement.Domain.AggregateModels;
using UserManagement.Domain.Exceptions;
using UserManagement.Domain.Models;

namespace UserManagement.Application.Features.Commands.UserInfo.UserInfoRegister;

public class UserInfoRegisterCommandHandler : IRequestHandler<UserInfoRegisterCommand, ServiceResult<string>>
{
    private readonly IUserInfoRepository _userInfoRepository;
   
    public UserInfoRegisterCommandHandler(IUserInfoRepository userInfoRepository)
    {
        _userInfoRepository = userInfoRepository;
    }
    public  async Task<ServiceResult<string>> Handle(UserInfoRegisterCommand request, CancellationToken cancellationToken)
    {
        ServiceResult<string> result = new ServiceResult<string>();
        try
        {

            var user = new Domain.AggregateModels.UserInfo(request.Id, request.Username, request.FirstName,
                request.LastName, request.Email, UserStatus.Pending);
           
            var data = await _userInfoRepository.AddAsync(user);
            var response = await _userInfoRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            if (response>0)
            {
               
                result.Result = BaseMessages.UserRegisterMessages.UserRegisterSucceess;
                result.IsSucceeded = true;
            }
            else
            {
                result.Result = BaseMessages.UserRegisterMessages.UserRegisterFail;
                result.IsSucceeded = false;
            }

        }
        catch (Exception e)
        {
            throw new ApiException(HttpStatusCode.InternalServerError, BaseMessages.UserRegisterMessages.UserRegisterFail);
        }

        return result;


    }
}
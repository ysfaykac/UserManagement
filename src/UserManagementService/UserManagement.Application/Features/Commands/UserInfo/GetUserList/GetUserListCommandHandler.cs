using AutoMapper;
using MediatR;
using UserManagement.Application.Abstract;
using UserManagement.Application.Dtos;
using UserManagement.Domain.Abstract;

namespace UserManagement.Application.Features.Commands.UserInfo.GetUserList;

public class GetUserListCommandHandler:IRequestHandler<GetUserListCommand,List<UserInfoDto>>
{
    private readonly IUserInfoRepository _userInfoRepository;
    private readonly IMapper _mapper;

    public GetUserListCommandHandler(IUserInfoRepository userInfoRepository,IMapper mapper)
    {
        _userInfoRepository = userInfoRepository;
        _mapper = mapper;
    }
    public async Task<List<UserInfoDto>> Handle(GetUserListCommand request, CancellationToken cancellationToken)
    {
        var list = await _userInfoRepository.GetAll();
        var data = _mapper.Map<List<UserInfoDto>>(list);
        return data;
    }
}
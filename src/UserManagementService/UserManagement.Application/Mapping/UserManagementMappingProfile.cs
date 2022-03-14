using AutoMapper;
using UserManagement.Application.Dtos;
using UserManagement.Domain.AggregateModels;

namespace UserManagement.Application.Mapping;

public class UserManagementMappingProfile:Profile
{
    public UserManagementMappingProfile()
    {
        CreateMap<UserInfoDto, UserInfo>().ReverseMap();
    }
}
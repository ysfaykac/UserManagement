using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using Moq;
using UserManagement.Application.Abstract;
using UserManagement.Domain.AggregateModels;

namespace UserManagement.Test;

public class UserInfoRepositoryMock
{
    public static Mock<IUserInfoRepository> GetUserInfoRepository()
    {
        var userInfos = new List<UserInfo>
            {
                new UserInfo
                {
                    Id = Guid.Parse("f420f49e-eb78-4392-affd-0bf892e6ce62"),
                    UserStatus = UserStatus.Pending,
                    UserName = "johndoe",
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "john@doe.com",
                    CreateDate = DateTime.Now.AddYears(-2)

                },
                new UserInfo
                {
                    Id = Guid.Parse("97893078-6666-49a4-8937-34c359db7ed6"),
                    UserStatus = UserStatus.Pending,
                    UserName = "johndoe1",
                    FirstName = "John1",
                    LastName = "Doe",
                    Email = "john@doe.com",
                    CreateDate = DateTime.Now.AddYears(-2)

                },
                new UserInfo
                {  Id = Guid.Parse("953b30e7-44f2-4682-940f-06e4f5cda175"),
                    UserStatus = UserStatus.Pending,
                    UserName = "johndoe2",
                    FirstName = "John2",
                    LastName = "Doe",
                    Email = "john@doe.com",
                    CreateDate = DateTime.Now.AddYears(-2)
                },
                new UserInfo
                {
                    Id = Guid.Parse("ffb5cab0-7ea2-4d3c-ae91-b1ed2bcd0223"),
                    UserStatus = UserStatus.Pending,
                    UserName = "johndoe3",
                    FirstName = "John3",
                    LastName = "Doe",
                    Email = "john@doe.com",
                    CreateDate = DateTime.Now.AddYears(-2)
                }
            };


        var mockedUserInfoRepository = new Mock<IUserInfoRepository>();
        mockedUserInfoRepository.Setup(userInfoRepository => userInfoRepository.GetAll()).ReturnsAsync(userInfos);

        mockedUserInfoRepository.Setup(userInfoRepository => userInfoRepository.AddAsync(It.IsAny<UserInfo>())).ReturnsAsync(((UserInfo userInfo) => userInfo));
        mockedUserInfoRepository.Setup(userInfoRepository => userInfoRepository.GetById(It.IsAny<Guid>())).ReturnsAsync((Guid id) => userInfos.Single(t => t.Id == id));

        mockedUserInfoRepository.Setup(userInfoRepository => userInfoRepository.GetSingleAsync(It.IsAny<Expression<Func<UserInfo, bool>>>()))
            .ReturnsAsync((Expression<Func<UserInfo, bool>> filter) => userInfos.First(filter.Compile()));

        mockedUserInfoRepository.Setup(t => t.UnitOfWork.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(1);



        return mockedUserInfoRepository;
    }
}
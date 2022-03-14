using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using UserManagement.Application.Abstract;

namespace UserManagement.Test
{
    [TestClass]
    public class UserManagementTest
    {
        private readonly Mock<IUserInfoRepository> _userInfoRepository;

        public UserManagementTest()
        {
            _userInfoRepository = UserInfoRepositoryMock.GetUserInfoRepository();
        }

        [TestMethod]
        public async Task get_userinfo_by_id()
        {
            var customer = await _userInfoRepository.Object.GetSingleAsync(t => t.Id ==new Guid("f420f49e-eb78-4392-affd-0bf892e6ce62"));
            Assert.IsNotNull(customer);
        }
    }
}
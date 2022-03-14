using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using UserService.Application.Abstract;

namespace UserService.Test
{
    [TestClass]
    public class UserServiceTest
    {
        private readonly Mock<IUserRepository> _userRepository;

        public UserServiceTest()
        {
            _userRepository = UserRepositoryMock.GetUserRepository();
        }

        [TestMethod]
        public async Task get_user_by_id()
        {
            var customer = await _userRepository.Object.GetSingleAsync(t => t.Id ==new Guid("f420f49e-eb78-4392-affd-0bf892e6ce62"));
            Assert.IsNotNull(customer);
        }
    }
}
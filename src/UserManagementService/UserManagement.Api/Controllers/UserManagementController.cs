using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Api.Services;

namespace UserManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserManagementController : ControllerBase
    {
        private readonly IUserManagementService _userManagementService;

        public UserManagementController(IUserManagementService userManagementService)
        {
            _userManagementService = userManagementService;
        }

        [HttpPost("Accept/{userId}")]
        [AllowAnonymous]
        public async Task<IActionResult> Accept(Guid userId)
        {
            var res = await _userManagementService.AcceptRegistration(userId);
            return Ok(res);
        }
        [HttpPost("Decline/{userId}")]
        [AllowAnonymous]
        public async Task<IActionResult> Decline(Guid userId)
        {
            var res = await _userManagementService.DeclineRegistration(userId);
            return Ok(res);
        }

        [HttpPost("Enable/{userId}")]
        [AllowAnonymous]
        public async Task<IActionResult> Enable(Guid userId)
        {
            var res = await _userManagementService.EnableUser(userId);
            return Ok(res);
        }

        [HttpPost("Disable/{userId}")]
        [AllowAnonymous]
        public async Task<IActionResult> Disable(Guid userId)
        {
            var res = await _userManagementService.DisableUser(userId);
            return Ok(res);
        }

        [HttpPost("GetUserList")]
        [AllowAnonymous]
        public async Task<IActionResult> GetUserList()
        {
            var res = await _userManagementService.GetUserList();
            return Ok(res);
        }
    }
}

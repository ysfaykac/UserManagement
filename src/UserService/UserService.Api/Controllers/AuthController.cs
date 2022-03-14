using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserService.Api.Model;
using UserService.Application.Features.Commands.User;
using UserService.Application.Features.Queries.User;

namespace UserService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginRequestModel model)
        {
            var res = await _mediator.Send(new LoginRequestQuery(model.UserName,model.Password));
            return Ok(res);
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody]RegisterModel model)
        {
            var res = await _mediator.Send(new  UserRegisterCommand(model.Username, model.Password,model.FirstName,model.LastName,model.Email));
            return Ok(res);

        }
    }
}

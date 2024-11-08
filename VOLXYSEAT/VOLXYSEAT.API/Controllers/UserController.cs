using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VOLXYSEAT.API.Application.Commands.Account.Login;
using VOLXYSEAT.API.Application.Commands.Account.Logout;
using VOLXYSEAT.API.Application.Commands.Account.Register;

namespace VOLXYSEAT.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("new-user")]
        //[ProducesResponseType(typeof(UserViewModel), (int)HttpStatusCode.OK)]
        //[ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post([FromBody] RegisterUserCommand request)
        {
            var result = await _mediator.Send(request);
            return result ? Ok() : BadRequest();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand request)
        {
            var result = await _mediator.Send(request);
            return result != null ? Ok(result) : BadRequest();
        }


        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {

            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var result = await _mediator.Send(new LogoutCommand(Guid.Parse(userId)));

            if(result)
                return Ok(new {message = "Logout successful."});

            return BadRequest(new { message = "Failed to logout." });
        }
    }
}

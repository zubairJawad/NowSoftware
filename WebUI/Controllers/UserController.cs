using Application.DTOs;
using Application.Users.Commands;
using Application.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("signup")]
        public async Task<ActionResult> SignUp(SignUpCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }
        [HttpPost("authenticate")]
        public async Task<ActionResult<UserDTO>> AuthenticateUser(AuthenticateUserCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("auth/balance")]
        public async Task<ActionResult<UserBalanceDTO>> GetBalance([FromQuery] GetBalanceQuery query)
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }
    }
}

using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Courses.Application.Accounts.Commands;
using Courses.WebAPI.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Courses.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : BaseController
    {
        // POST api/accounts
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateAccountCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        // PUT api/accounts/confirmemail
        [HttpPut]
        [Authorize]
        [Route("confirmemail")]
        public async Task<ActionResult> ConfirmEmail([FromQuery] string token)
        {
            var command = new ConfirmAccountEmailCommand
            {
                Token = token,
                UserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier))
            };

            return Ok(await Mediator.Send(command));
        }
    }
}

using System.Threading.Tasks;
using Courses.Application.Auth.Queries;
using Courses.WebAPI.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Courses.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : BaseController
    {
        // GET api/auth/login
        [HttpGet("login")]
        public async Task<IActionResult> Post([FromQuery] GetTokenQuery credentials)
        {
            return Ok(await Mediator.Send(credentials));
        }        
    }   
}
using System;
using System.Linq;
using System.Threading.Tasks;
using Courses.Application.Courses.Commands.Create;
using Courses.Application.Courses.Commands.Delete;
using Courses.Application.Courses.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Courses.WebAPI.Controllers
{
    public class CoursesController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await Mediator.Send(new GetCoursesQuery()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await Mediator.Send(new GetCoursePreviewQuery {Id = id}));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromForm] CreateCourseCommand command)
        {
            var a = User.Identity.Name;
            return Ok(await Mediator.Send(command));
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await Mediator.Send(new DeleteCourseCommand {Id = id}));
        }
    }
}
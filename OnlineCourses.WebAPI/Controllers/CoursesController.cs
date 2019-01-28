using System;
using System.Threading.Tasks;
using Courses.Application.Courses.Commands;
using Courses.Application.Courses.Commands.Create;
using Courses.Application.Courses.Commands.Delete;
using Courses.Application.Courses.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Courses.WebAPI.Controllers
{
    public class CoursesController : BaseController
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await Mediator.Send(new GetCoursePreviewQuery {Id = id}));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateCourseCommand command)
        {
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
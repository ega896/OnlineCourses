using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using OnlineCourses.Application.Courses.Commands;
using OnlineCourses.Application.Courses.Queries;

namespace OnlineCourses.WebAPI.Controllers
{
    public class CoursesController : BaseController
    {
        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await Mediator.Send(new GetCoursePreviewQuery()));
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateCourseCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

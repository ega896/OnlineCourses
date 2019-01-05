using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using System.Threading.Tasks;
using OnlineCourses.Application;

namespace OnlineCourses.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : BaseController
    {
        // GET api/values
        [HttpGet]
        public async Task<IActionResult> GetCategoryPreview([FromQuery] GetCoursePreviewQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
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

using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Courses.Application.Courses.Queries;
using Courses.Application.Extensions;
using Courses.Persistence;
using Courses.Tests.Infrastructure;
using Xunit;

namespace Courses.Tests.Courses
{
    [Collection("QueryCollection")]
    public class CoursesQueriesTests
    {
        private readonly ApplicationDbContext _context;

        public CoursesQueriesTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
        }

        [Fact]
        public async Task GetCoursesQueryTest()
        {
            var handler = new GetCoursesQueryHandler(_context);

            var result = await handler.Handle(new GetCoursesQuery {PageNumber = 1, PageSize = 50}, CancellationToken.None);

            Assert.IsType<PagedResult<CoursePreviewDto>>(result);
            Assert.Equal(_context.Courses.Count(), result.Results.Count);
        }

        [Fact]
        public async Task GetCourseQueryTest()
        {
            var handler = new GetCoursePreviewQueryHandler(_context);
            var firstCourseId = _context.Courses.First().Id;

            var result = await handler.Handle(new GetCoursePreviewQuery {Id = firstCourseId},
                CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(_context.Courses.Find(firstCourseId).Name, result.Name);
        }
    }
}
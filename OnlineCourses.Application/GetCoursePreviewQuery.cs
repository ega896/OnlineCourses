using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace OnlineCourses.Application
{
    public class GetCoursePreviewQuery : IRequest<List<CoursePreviewDto>>
    {
        public int Id { get; set; }
    }

    public class GetCoursePreviewQueryHandler : IRequestHandler<GetCoursePreviewQuery, List<CoursePreviewDto>>
    {
        public Task<List<CoursePreviewDto>> Handle(GetCoursePreviewQuery request, CancellationToken cancellationToken)
        {
            var courses = new List<CoursePreviewDto>
            {
                new CoursePreviewDto
                {
                    Description = "Intermediate",
                    Id = 1,
                    Name = "English"
                },
                new CoursePreviewDto
                {
                    Description = "I am superman",
                    Id = 2,
                    Name = "Psychology"
                }
            };

            return Task.Run(() => courses, cancellationToken);
        }
    }

    public class CoursePreviewDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
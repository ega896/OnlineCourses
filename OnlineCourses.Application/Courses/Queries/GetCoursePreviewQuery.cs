using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using OnlineCourses.Persistence;

namespace OnlineCourses.Application.Courses.Queries
{
    public class GetCoursePreviewQuery : IRequest<List<CoursePreviewDto>>
    {
    }

    public class GetCoursePreviewQueryHandler : IRequestHandler<GetCoursePreviewQuery, List<CoursePreviewDto>>
    {
        private readonly OnlineCoursesDbContext _context;

        public GetCoursePreviewQueryHandler(OnlineCoursesDbContext context)
        {
            _context = context;
        }

        public Task<List<CoursePreviewDto>> Handle(GetCoursePreviewQuery request, CancellationToken cancellationToken)
        {
            // Add projection and fetch all from database

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
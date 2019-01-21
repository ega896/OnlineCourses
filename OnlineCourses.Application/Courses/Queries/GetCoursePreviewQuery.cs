using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OnlineCourses.Persistence;

namespace OnlineCourses.Application.Courses.Queries
{
    public class GetCoursePreviewQuery : IRequest<List<CoursePreviewDto>>
    {
    }

    public class GetCoursePreviewQueryHandler : IRequestHandler<GetCoursePreviewQuery, List<CoursePreviewDto>>
    {
        private readonly ApplicationDbContext _context;

        public GetCoursePreviewQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<List<CoursePreviewDto>> Handle(GetCoursePreviewQuery request, CancellationToken cancellationToken)
        {
            return _context.Courses
                .Select(CoursePreviewDto.Projection)
                .ToListAsync(cancellationToken);
        }
    }
}
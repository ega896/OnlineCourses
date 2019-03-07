using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Courses.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Courses.Application.Courses.Queries
{
    public class GetCoursesQuery : IRequest< IEnumerable<CoursePreviewDto>>
    {
    }

    public class GetCoursesQueryHandler : IRequestHandler<GetCoursesQuery, IEnumerable<CoursePreviewDto>>
    {
        private readonly ApplicationDbContext _context;

        public GetCoursesQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CoursePreviewDto>> Handle(GetCoursesQuery request, CancellationToken cancellationToken)
        {
            return await _context.Courses
                .Select(CoursePreviewDto.Projection).ToListAsync(cancellationToken);
        }
    }
}
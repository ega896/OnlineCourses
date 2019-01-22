using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Courses.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Courses.Application.Courses.Queries
{
    public class GetCoursePreviewQuery : IRequest<CoursePreviewDto>
    {
        public Guid Id { get; set; }
    }

    public class GetCoursePreviewQueryHandler : IRequestHandler<GetCoursePreviewQuery, CoursePreviewDto>
    {
        private readonly ApplicationDbContext _context;

        public GetCoursePreviewQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<CoursePreviewDto> Handle(GetCoursePreviewQuery request, CancellationToken cancellationToken)
        {
            return _context.Courses
                .Select(CoursePreviewDto.Projection)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        }
    }
}
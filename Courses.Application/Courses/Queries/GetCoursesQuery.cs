using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Courses.Persistence;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Courses.Application.Courses.Queries
{
    public class GetCoursesQuery : IRequest< IEnumerable<CoursePreviewDto>>
    {
        public bool IsUserOnly { get; set; }
    }

    public class GetCoursesQueryHandler : IRequestHandler<GetCoursesQuery, IEnumerable<CoursePreviewDto>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetCoursesQueryHandler(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<CoursePreviewDto>> Handle(GetCoursesQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Courses.AsNoTracking();
            var userId = Guid.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (request.IsUserOnly)
            {
                query = query.Where(x => x.UserId == userId);
            }

            return await query.Select(CoursePreviewDto.Projection).ToListAsync(cancellationToken);
        }
    }
}
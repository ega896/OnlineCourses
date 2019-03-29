using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Courses.Application.Extensions;
using Courses.Infrastructure.Extensions;
using Courses.Persistence;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Courses.Application.Courses.Queries
{
    public class GetCoursesQuery : IRequest<PagedResult<CoursePreviewDto>>
    {
        public bool IsUserOnly { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }
    }

    public class GetCoursesQueryHandler : IRequestHandler<GetCoursesQuery, PagedResult<CoursePreviewDto>>
    {
        private readonly ApplicationDbContext _context;
        //private readonly IHttpContextAccessor _httpContextAccessor;

        public GetCoursesQueryHandler(ApplicationDbContext context/*, IHttpContextAccessor httpContextAccessor*/)
        {
            _context = context;
            //_httpContextAccessor = httpContextAccessor;
        }

        public async Task<PagedResult<CoursePreviewDto>> Handle(GetCoursesQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Courses.AsNoTracking();
            //var userId = _httpContextAccessor.CurrentUser();

            //if (request.IsUserOnly)
            //{
            //    query = query.Where(x => x.UserId == userId);
            //}

            return await query.Select(CoursePreviewDto.Projection).GetPaged(request.PageNumber, request.PageSize);
        }
    }
}
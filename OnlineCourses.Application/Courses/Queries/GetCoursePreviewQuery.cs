using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OnlineCourses.Domain.Entities;
using OnlineCourses.Persistence;

namespace OnlineCourses.Application.Courses.Queries
{
    public class GetCoursePreviewQuery : IRequest<List<CoursePreviewDto>>
    {
    }

    public class GetCoursePreviewQueryHandler : IRequestHandler<GetCoursePreviewQuery, List<CoursePreviewDto>>
    {
        private readonly CoursesDbContext _context;

        public GetCoursePreviewQueryHandler(CoursesDbContext context)
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

    public class CoursePreviewDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public static Expression<Func<Course, CoursePreviewDto>> Projection
        {
            get
            {
                return c => new CoursePreviewDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description
                };
            }
        }
    }
}
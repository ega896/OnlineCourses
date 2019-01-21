using System;
using System.Linq.Expressions;
using OnlineCourses.Domain.Entities;

namespace OnlineCourses.Application.Courses.Queries
{
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
using System.Collections.Generic;
using MediatR;

namespace OnlineCourses.Application
{
    public class GetCoursePreviewQuery : IRequest<List<CoursePreviewDto>>
    {
        public int Id { get; set; }
    }

    public class CoursePreviewDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OnlineCourses.Domain.Entities;
using OnlineCourses.Persistence;

namespace OnlineCourses.Application.Courses.Commands
{
    public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, Unit>
    {
        private readonly CoursesDbContext _context;

        public CreateCourseCommandHandler(CoursesDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
            var entity = new Course
            {
                Name = request.Name,
                Description = request.Description
            };

            _context.Courses.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }

    public class CreateCourseCommand : IRequest
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
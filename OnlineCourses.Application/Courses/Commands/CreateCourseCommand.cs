using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using OnlineCourses.Domain.Entities;
using OnlineCourses.Persistence;

namespace OnlineCourses.Application.Courses.Commands
{
    public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, Unit>
    {
        private readonly OnlineCoursesDbContext _context;

        public CreateCourseCommandHandler(OnlineCoursesDbContext context)
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

    public class CreateCourseCommandValidator : AbstractValidator<CreateCourseCommand>
    {
        public CreateCourseCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().Length(10, 30).WithMessage("Name validation failed");
            RuleFor(x => x.Description).Length(10, 260);
        }
    }
}

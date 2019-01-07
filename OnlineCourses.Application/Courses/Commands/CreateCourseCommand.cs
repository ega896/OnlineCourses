using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;

namespace OnlineCourses.Application.Courses.Commands
{
    public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, Unit>
    {
        public Task<Unit> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
            var entity = new Course
            {
                Name = request.Name,
                Description = request.Description
            };

            // add in db logic
            //_context.Customers.Add(entity);
            //await _context.SaveChangesAsync(cancellationToken);

            return Task.Run(() => Unit.Value, cancellationToken);
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

    public class Course
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}

using FluentValidation;

namespace OnlineCourses.Application.Courses.Commands
{
    public class CreateCourseCommandValidator : AbstractValidator<CreateCourseCommand>
    {
        public CreateCourseCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().Length(10, 30).WithMessage("Name validation failed");
            RuleFor(x => x.Description).Length(10, 260);
        }
    }
}
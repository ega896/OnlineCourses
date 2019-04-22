using System.Linq;
using Courses.Domain.Constants;
using FluentValidation;

namespace Courses.Application.Courses.Commands.Create
{
    public class CreateCourseCommandValidator : AbstractValidator<CreateCourseCommand>
    {
        public CreateCourseCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().Length(10, 30).WithMessage("Name validation failed");
            RuleFor(x => x.Description).Length(10, 260).WithMessage("Description validation failed");
            RuleFor(x => x.Languages).Must(x => x.All(l => SupportedLanguages.GetAll().Contains(l))).NotNull();
            RuleFor(x => x.Avatar).NotNull();
        }
    }
}
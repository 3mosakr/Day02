using Day02.ViewModel.CourseViewModel;
using FluentValidation;

namespace Day02.Validations
{
    public class CreateCourseValidator : AbstractValidator<CreateCourseViewModel>
    {

        public CreateCourseValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Course name is required.")
                .MaximumLength(100).WithMessage("Course name must not exceed 100 characters.")
                .MinimumLength(2).WithMessage("Course name must be more than 2 characters.");

            RuleFor(x => x.Degree)
                .InclusiveBetween(50, 100).WithMessage("Degree must be between 50 and 100.");


        }
    }
}

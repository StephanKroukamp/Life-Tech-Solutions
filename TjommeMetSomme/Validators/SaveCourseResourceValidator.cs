using FluentValidation;
using TjommeMetSomme.Resources;

namespace TjommeMetSomme.Validators
{
    public class SaveCourseResourceValidator : AbstractValidator<SaveCourseResource>
    {
        public SaveCourseResourceValidator()
        {
            RuleFor(saveCourseResource => saveCourseResource.Name)
                .NotEmpty()
                .MaximumLength(50);

            //RuleFor(m => m.)
            //    .NotEmpty()
            //    .WithMessage("'Parent Id' must not be 0.");
        }
    }
}
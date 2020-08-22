using FluentValidation;
using TjommeMetSomme.Resources;

namespace TjommeMetSomme.Validators
{
    public class SaveStudentResourceValidator : AbstractValidator<SaveStudentResource>
    {
        public SaveStudentResourceValidator()
        {
            RuleFor(m => m.FirstName)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(m => m.LastName)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(m => m.ParentId)
                .NotEmpty()
                .WithMessage("'Parent Id' must not be 0.");
        }
    }
}
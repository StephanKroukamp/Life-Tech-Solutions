using FluentValidation;
using TjommeMetSomme.Resources;

namespace TjommeMetSomme.Validators
{
    public class SaveStudentResourceValidator : AbstractValidator<SaveStudentResource>
    {
        public SaveStudentResourceValidator()
        {
            RuleFor(saveStudentResource => saveStudentResource.FirstName)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(saveStudentResource => saveStudentResource.LastName)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(saveStudentResource => saveStudentResource.ParentId)
                .NotEmpty()
                .WithMessage("'Parent Id' must not be 0.");
        }
    }
}
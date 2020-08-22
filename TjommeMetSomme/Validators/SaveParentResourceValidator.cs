using FluentValidation;
using TjommeMetSomme.Resources;

namespace TjommeMetSomme.Validators
{
    public class SaveParentResourceValidator : AbstractValidator<SaveParentResource>
    {
        public SaveParentResourceValidator()
        {
            RuleFor(a => a.FirstName)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(a => a.LastName)
                .NotEmpty()
                .MaximumLength(50);
        }
    }
}
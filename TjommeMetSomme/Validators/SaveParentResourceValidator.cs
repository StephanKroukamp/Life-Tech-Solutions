using FluentValidation;
using TjommeMetSomme.Resources;

namespace TjommeMetSomme.Validators
{
    public class SaveParentResourceValidator : AbstractValidator<SaveParentResource>
    {
        public SaveParentResourceValidator()
        {
            RuleFor(saveParentResource => saveParentResource.FirstName)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(saveParentResource => saveParentResource.LastName)
                .NotEmpty()
                .MaximumLength(50);
        }
    }
}
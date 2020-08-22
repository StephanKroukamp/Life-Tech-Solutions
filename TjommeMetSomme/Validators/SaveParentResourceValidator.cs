using FluentValidation;
using TjommeMetSomme.Resources;

namespace TjommeMetSomme.Validators
{
    public class SaveParentResourceValidator : AbstractValidator<SaveParentResource>
    {
        public SaveParentResourceValidator()
        {
            RuleFor(savePersonResource => savePersonResource.Email)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(savePersonResource => savePersonResource.UserName)
                    .NotEmpty()
                    .MaximumLength(50);

            RuleFor(savePersonResource => savePersonResource.FirstName)
                    .NotEmpty()
                    .MaximumLength(50);

            RuleFor(savePersonResource => savePersonResource.LastName)
                    .NotEmpty()
                    .MaximumLength(50);

            RuleFor(savePersonResource => savePersonResource.Password)
                .NotEmpty()
                .MaximumLength(50);
        }
    }
}
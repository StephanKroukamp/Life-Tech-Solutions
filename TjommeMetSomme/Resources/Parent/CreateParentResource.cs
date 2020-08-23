using FluentValidation;

namespace TjommeMetSomme.Resources
{
    public class CreateParentResourceValidator : AbstractValidator<CreateParentResource>
    {
        public CreateParentResourceValidator()
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

    public class CreateParentResource
    {
        public string Email { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Password { get; set; }
    }
}
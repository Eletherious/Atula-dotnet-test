using AtulaTestWebsite.Models.Modles;
using FluentValidation;

namespace AtulaTestWebsite.Models.Validators
{
    public class ApplicationUserValidator : AbstractValidator<ApplicationUser>
    {
        public ApplicationUserValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required")
                .MaximumLength(50).WithMessage("First name cannot be longer than 50 characters")
                .Must(name => name.All(char.IsLetter)).WithMessage("First name must contain only letters");RuleFor(x => x.LastName);

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Surname is required")
                .MaximumLength(50).WithMessage("Surname cannot be longer than 50 characters")
                .Must(name => name.All(char.IsLetter)).WithMessage("Surname must contain only letters"); RuleFor(x => x.LastName);

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email address");

            // .Identity already has validation checks for the password
        }
    }
}

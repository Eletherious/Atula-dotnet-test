using AtulaTestWebsite.Models.Modles;
using FluentValidation;

namespace AtulaTestWebsite.Models.Validators
{
    public class CategoryValidator : AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Category name is required")
                .MaximumLength(50).WithMessage("Category name must be less than 50 characters")
                .Must(name => name.All(char.IsLetter)).WithMessage("The category name must contain only letters"); RuleFor(x => x.Name);

        }
    }
}

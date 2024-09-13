using AtulaTestWebsite.Models.ViewModels;
using FluentValidation;

namespace AtulaTestWebsite.Models.Validators
{
    public class CategoryVMValidator : AbstractValidator<CategoryVM>
    {
        public CategoryVMValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Category name is required")
                .MaximumLength(50).WithMessage("Category name must be less than 50 characters")
                .Must(name => name.All(char.IsLetter)).WithMessage("The category name must contain only letters"); RuleFor(x => x.Name); ;

            RuleFor(c => c.Id)
                .GreaterThan(0).WithMessage("Category Id must be greater than 0");
        }
    }
}

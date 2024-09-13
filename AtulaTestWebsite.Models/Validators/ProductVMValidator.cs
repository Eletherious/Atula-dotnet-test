using AtulaTestWebsite.Models.ViewModels;
using FluentValidation;

namespace AtulaTestWebsite.Models.Validators
{
    public class ProductVMValidator : AbstractValidator<ProductVM>
    {
        public ProductVMValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("Product name is required")
                .MaximumLength(50).WithMessage("Product name must be less than 50 characters")
                .Must(name => name.All(char.IsLetter)).WithMessage("The category name must contain only letters"); RuleFor(x => x.Name); ;

            RuleFor(p => p.Sku)
                .NotEmpty().WithMessage("Product SKU is required")
                .MaximumLength(20).WithMessage("SKU must be less than 20 characters")
                .Must(sku => sku.All(char.IsLetterOrDigit)).WithMessage("SKU must be alphanumeric")
                .Equal(p => p.Sku.ToUpper()).WithMessage("SKU must be in uppercase");

            RuleFor(p => p.CategoryId)
                .GreaterThan(0).WithMessage("Please select a category.");
        }
    }
}

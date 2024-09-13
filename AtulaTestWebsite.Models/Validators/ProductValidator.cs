using AtulaTestWebsite.Models.Modles;
using FluentValidation;

namespace AtulaTestWebsite.Models.Validators
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("Product name is required")
                .MaximumLength(50).WithMessage("Product name must be less than 50 characters");

            RuleFor(p => p.Sku)
                .NotEmpty().WithMessage("Product SKU is required")
                .MaximumLength(20).WithMessage("SKU must be less than 20 characters")
                .Must(sku => sku.All(char.IsLetter)).WithMessage("The SKU must only use letters"); RuleFor(x => x.Sku);

            RuleFor(p => p.ProductCategories)
                .NotEmpty().WithMessage("There has to be a category assigned");
        }
    }
}

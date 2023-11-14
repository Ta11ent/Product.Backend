using FluentValidation;
using ProductCatalog.cs.Models.Product;

namespace ProductCatalog.APIcs.Validation.Product
{
    public class CreateProductValidator : AbstractValidator<CreateProductDto>
    {
        public CreateProductValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Description).NotEmpty().MaximumLength(300);    
        }
    }
}
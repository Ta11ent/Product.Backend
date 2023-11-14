using FluentValidation;
using ProductCatalog.cs.Models.Category;

namespace ProductCatalog.APIcs.Validation.Category
{
    public class UpdateCategoryValidator : AbstractValidator<UpdateCategoryDto>
    {
        public UpdateCategoryValidator() 
        { 
            RuleFor(x => x.CategoryId).NotEqual(Guid.Empty);
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Name).MaximumLength(300);
        }
    }
}

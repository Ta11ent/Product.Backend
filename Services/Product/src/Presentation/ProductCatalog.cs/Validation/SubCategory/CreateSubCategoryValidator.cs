using ProductCatalog.API.Models.SubCategory;

namespace ProductCatalog.API.Validation.SubCategory
{
    public class CreateSubCategoryValidator : AbstractValidator<CreateSubCategoryDto>
    {
        public CreateSubCategoryValidator() 
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Description).NotEmpty().MaximumLength(300);
        }
    }
}

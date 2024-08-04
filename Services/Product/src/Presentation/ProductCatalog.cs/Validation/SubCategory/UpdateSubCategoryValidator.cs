using ProductCatalog.API.Models.SubCategory;

namespace ProductCatalog.API.Validation.SubCategory
{
    public class UpdateSubCategoryValidator : AbstractValidator<UpdateSubCategoryDto>
    {
        public UpdateSubCategoryValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Description).NotEmpty().MaximumLength(300);
        }
    }
}

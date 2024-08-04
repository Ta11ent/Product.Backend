using ProductCatalog.API.Models.ROE;

namespace ProductCatalog.API.Validation.ROE
{
    public class UpdateROEValidator : AbstractValidator<UpdateROEDto>
    {
        public UpdateROEValidator()
        {
            RuleFor(x => x.Rate).NotNull();
            RuleFor(x => x.DateFrom).NotEmpty();
        }
    }
}

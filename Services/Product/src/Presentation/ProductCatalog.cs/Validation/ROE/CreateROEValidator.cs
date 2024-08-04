using ProductCatalog.API.Models.ROE;

namespace ProductCatalog.API.Validation.ROE
{
    public class CreateROEValidator : AbstractValidator<CreateROEDto>
    {
        public CreateROEValidator() {
            RuleFor(x => x.Rate).NotNull();
            RuleFor(x => x.DateFrom).NotEmpty();
        }
    }
}

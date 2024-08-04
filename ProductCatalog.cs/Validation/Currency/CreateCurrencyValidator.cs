using ProductCatalog.API.Models.Currecny;

namespace ProductCatalog.API.Validation.Currency
{
    public class CreateCurrencyValidator : AbstractValidator<CreateCurrencyDto>
    {
        public CreateCurrencyValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Code).NotEmpty().MaximumLength(3);
        }
    }
}

using FluentValidation;
using OrderFlow.Api.Contracts.Products;

namespace OrderFlow.Api.Validation;

public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
{
    public CreateProductRequestValidator()
    {
        RuleFor(x => x.Sku).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Name).NotEmpty().MaximumLength(200);
        RuleFor(x => x.UnitPrice).GreaterThan(0);
        RuleFor(x => x.VatRate).InclusiveBetween(0, 100);
    }
}

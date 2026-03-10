using FluentValidation;
using OrderFlow.Api.Contracts.Orders;

namespace OrderFlow.Api.Validation;

public class AddOrderLineRequestValidator : AbstractValidator<AddOrderLineRequest>
{
    public AddOrderLineRequestValidator()
    {
        RuleFor(x => x.Sku).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Description).NotEmpty().MaximumLength(500);
        RuleFor(x => x.Quantity).GreaterThan(0);
        RuleFor(x => x.UnitPrice).GreaterThan(0);
    }
}

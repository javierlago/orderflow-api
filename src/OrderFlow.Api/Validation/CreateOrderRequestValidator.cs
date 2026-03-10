using FluentValidation;
using OrderFlow.Api.Contracts.Orders;

namespace OrderFlow.Api.Validation;

public class CreateOrderRequestValidator : AbstractValidator<CreateOrderRequest>
{
    public CreateOrderRequestValidator()
    {
        RuleFor(x => x.CustomerId).NotEmpty();
    }
}

using FluentValidation;
using OrderFlow.Api.Contracts.Customers;

namespace OrderFlow.Api.Validation;

public class CreateCustomerRequestValidator : AbstractValidator<CreateCustomerRequest>
{
    public CreateCustomerRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(200);
        RuleFor(x => x.TaxId).MaximumLength(20);
        RuleFor(x => x.Email).MaximumLength(320).EmailAddress().When(x => !string.IsNullOrWhiteSpace(x.Email));
    }
}

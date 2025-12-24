namespace OrderFlow.Api.Contracts.Customers;

public record CustomerResponse(Guid Id, string Name, string? TaxId, string? Email);

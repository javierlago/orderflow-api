namespace OrderFlow.Api.Contracts.Customers;

public record CreateCustomerRequest(string Name, string? TaxId, string? Email);

namespace OrderFlow.Api.Contracts.Products;

public record ProductResponse(Guid Id, string Sku, string Name, decimal UnitPrice, decimal VatRate, bool IsActive);

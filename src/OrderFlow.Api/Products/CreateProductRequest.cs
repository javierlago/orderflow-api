namespace OrderFlow.Api.Contracts.Products;

public record CreateProductRequest(string Sku, string Name, decimal UnitPrice, decimal VatRate);

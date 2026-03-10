namespace OrderFlow.Api.Contracts.Orders;

public record AddOrderLineRequest(string Sku, string Description, int Quantity, decimal UnitPrice);

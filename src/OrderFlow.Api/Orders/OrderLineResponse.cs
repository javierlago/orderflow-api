namespace OrderFlow.Api.Contracts.Orders;

public record OrderLineResponse(Guid Id, string Sku, string Description, int Quantity, decimal UnitPrice);

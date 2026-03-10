namespace OrderFlow.Api.Contracts.Orders;

public record OrderResponse(Guid Id, Guid CustomerId, string Status, List<OrderLineResponse> Lines);

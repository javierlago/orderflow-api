namespace OrderFlow.Domain.Orders;

using OrderFlow.Domain.Common;

public sealed class Order
{
    public Guid Id { get; private set; }
    public Guid CustomerId { get; private set; }
    public OrderStatus Status { get; private set; }
    private readonly List<OrderLine> _lines = new();
    public IReadOnlyCollection<OrderLine> Lines => _lines.AsReadOnly();

    public Order(Guid customerId)
    {
        Id = Guid.NewGuid();
        CustomerId = customerId;
        Status = OrderStatus.Draft;
    }

    public void AddLine(
    string sku,
    string description,
    int quantity,
    decimal unitPrice)
    {
        if (Status != OrderStatus.Draft)
            throw new DomainException("Order is not editable unless it is in Draft status.");

        _lines.Add(new OrderLine(sku, description, quantity, unitPrice));
    }


    public void Confirm()
    {
        if (Status == OrderStatus.Confirmed)
            return; // idempotente: si ya está confirmado, no rompe

        if (Status == OrderStatus.Cancelled)
            throw new DomainException("Cannot confirm a cancelled order.");

        if (_lines.Count == 0)
            throw new DomainException("Cannot confirm an order with no lines.");

        Status = OrderStatus.Confirmed;
    }
    public void Cancel()
    {
        if (Status == OrderStatus.Cancelled)
            return; // idempotente

        Status = OrderStatus.Cancelled;
    }
   
}
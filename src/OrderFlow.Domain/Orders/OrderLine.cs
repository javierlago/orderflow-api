namespace OrderFlow.Domain.Orders;

public sealed class OrderLine
{
    public Guid Id { get; private set; }
    public string Sku { get; private set; }
    public string Description { get; private set; }
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }

    public OrderLine(
        string sku,
        string description,
        int quantity,
        decimal unitPrice)
    {
        Id = Guid.NewGuid();
        Sku = sku;
        Description = description;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }
}

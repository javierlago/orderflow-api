namespace OrderFlow.Domain.Products;

public class Product
{
    public Guid Id { get; private set; } = Guid.NewGuid();

    public string Sku { get; private set; } = default!;
    public string Name { get; private set; } = default!;

    public decimal UnitPrice { get; private set; }
    public decimal VatRate { get; private set; } // Ej: 21.00
    public bool IsActive { get; private set; } = true;

    public DateTime CreatedAtUtc { get; private set; } = DateTime.UtcNow;
    public DateTime? UpdatedAtUtc { get; private set; }

    private Product() { } // EF

    public Product(string sku, string name, decimal unitPrice, decimal vatRate)
    {
        Sku = sku.Trim().ToUpperInvariant();
        Name = name.Trim();
        UnitPrice = unitPrice;
        VatRate = vatRate;
    }

    public void Update(string sku, string name, decimal unitPrice, decimal vatRate, bool isActive)
    {
        Sku = sku.Trim().ToUpperInvariant();
        Name = name.Trim();
        UnitPrice = unitPrice;
        VatRate = vatRate;
        IsActive = isActive;
        UpdatedAtUtc = DateTime.UtcNow;
    }
}

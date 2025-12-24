namespace OrderFlow.Domain.Customers;

public class Customer
{
    public Guid Id { get; private set; } = Guid.NewGuid();

    public string Name { get; private set; } = default!;
    public string? TaxId { get; private set; }
    public string? Email { get; private set; }

    public DateTime CreatedAtUtc { get; private set; } = DateTime.UtcNow;
    public DateTime? UpdatedAtUtc { get; private set; }

    private Customer() { } // EF

    public Customer(string name, string? taxId, string? email)
    {
        Name = name.Trim();
        TaxId = string.IsNullOrWhiteSpace(taxId) ? null : taxId.Trim();
        Email = string.IsNullOrWhiteSpace(email) ? null : email.Trim();
    }

    public void Update(string name, string? taxId, string? email)
    {
        Name = name.Trim();
        TaxId = string.IsNullOrWhiteSpace(taxId) ? null : taxId.Trim();
        Email = string.IsNullOrWhiteSpace(email) ? null : email.Trim();
        UpdatedAtUtc = DateTime.UtcNow;
    }
}

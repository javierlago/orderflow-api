using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderFlow.Domain.Products;

namespace OrderFlow.Infrastructure.Persistence.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Sku).IsRequired().HasMaxLength(50);
        builder.HasIndex(x => x.Sku).IsUnique();

        builder.Property(x => x.Name).IsRequired().HasMaxLength(200);

        builder.Property(x => x.UnitPrice).HasColumnType("decimal(18,2)");
        builder.Property(x => x.VatRate).HasColumnType("decimal(5,2)");

        builder.Property(x => x.IsActive).IsRequired();

        builder.Property(x => x.CreatedAtUtc).IsRequired();
        builder.Property(x => x.UpdatedAtUtc);
    }
}

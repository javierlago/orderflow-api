using Microsoft.EntityFrameworkCore;
using OrderFlow.Domain.Customers;
using OrderFlow.Domain.Orders;
using OrderFlow.Domain.Products;

namespace OrderFlow.Infrastructure.Persistence;

public class OrderFlowDbContext : DbContext
{
    public OrderFlowDbContext(DbContextOptions<OrderFlowDbContext> options) : base(options) { }

    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderLine> OrderLines => Set<OrderLine>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderFlowDbContext).Assembly);
    }
}

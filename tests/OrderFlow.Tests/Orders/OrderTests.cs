using OrderFlow.Domain.Common;
using OrderFlow.Domain.Orders;
using Xunit;

namespace OrderFlow.Tests.Orders;

public sealed class OrderTests
{
    [Fact]
    public void Confirm_without_lines_should_throw_domain_exception()
    {
        // Arrange
        var order = new Order(Guid.NewGuid());

        // Act
        var ex = Assert.Throws<DomainException>(() => order.Confirm());

        // Assert
        Assert.Contains("no lines", ex.Message, StringComparison.OrdinalIgnoreCase);
    }
    [Fact]
    public void Draft_order_can_add_lines()
    {
        // Arrange
        var order = new Order(Guid.NewGuid());

        // Act
        order.AddLine(
            sku: "SKU-1",
            description: "Test product",
            quantity: 2,
            unitPrice: 10m);

        // Assert
        Assert.Single(order.Lines);
    }
    [Fact]
    public void Confirmed_order_cannot_add_lines()
    {
        // Arrange
        var order = new Order(Guid.NewGuid());
        order.AddLine("SKU-1", "Item", 1, 10m);
        order.Confirm();

        // Act
        var ex = Assert.Throws<DomainException>(() =>
            order.AddLine("SKU-2", "Another item", 1, 5m));

        // Assert
        Assert.Contains("not editable", ex.Message, StringComparison.OrdinalIgnoreCase);
    }

}

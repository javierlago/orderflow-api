using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderFlow.Api.Contracts.Orders;
using OrderFlow.Domain.Common;
using OrderFlow.Domain.Orders;
using OrderFlow.Infrastructure.Persistence;

namespace OrderFlow.Api.Controllers;

[ApiController]
[Route("api/orders")]
public class OrdersController : ControllerBase
{
    private readonly OrderFlowDbContext _db;

    public OrdersController(OrderFlowDbContext db)
    {
        _db = db;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateOrderRequest request, CancellationToken ct)
    {
        var customerExists = await _db.Customers.AnyAsync(x => x.Id == request.CustomerId, ct);
        if (!customerExists)
            return NotFound(new { error = "Customer not found." });

        var order = new Order(request.CustomerId);
        _db.Orders.Add(order);
        await _db.SaveChangesAsync(ct);

        return CreatedAtAction(nameof(GetById), new { id = order.Id }, MapToResponse(order));
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<OrderResponse>> GetById(Guid id, CancellationToken ct)
    {
        var order = await _db.Orders
            .AsNoTracking()
            .Include(x => x.Lines)
            .FirstOrDefaultAsync(x => x.Id == id, ct);

        if (order is null)
            return NotFound();

        return MapToResponse(order);
    }

    [HttpGet]
    public async Task<ActionResult<List<OrderResponse>>> List(CancellationToken ct)
    {
        var orders = await _db.Orders
            .AsNoTracking()
            .Include(x => x.Lines)
            .OrderByDescending(x => x.Id)
            .ToListAsync(ct);

        return orders.Select(MapToResponse).ToList();
    }

    [HttpPost("{id:guid}/lines")]
    public async Task<IActionResult> AddLine(Guid id, AddOrderLineRequest request, CancellationToken ct)
    {
        var order = await _db.Orders
            .Include(x => x.Lines)
            .FirstOrDefaultAsync(x => x.Id == id, ct);

        if (order is null)
            return NotFound();

        try
        {
            order.AddLine(request.Sku, request.Description, request.Quantity, request.UnitPrice);
            await _db.SaveChangesAsync(ct);
            return Ok(MapToResponse(order));
        }
        catch (DomainException ex)
        {
            return UnprocessableEntity(new { error = ex.Message });
        }
    }

    [HttpPost("{id:guid}/confirm")]
    public async Task<IActionResult> Confirm(Guid id, CancellationToken ct)
    {
        var order = await _db.Orders
            .Include(x => x.Lines)
            .FirstOrDefaultAsync(x => x.Id == id, ct);

        if (order is null)
            return NotFound();

        try
        {
            order.Confirm();
            await _db.SaveChangesAsync(ct);
            return NoContent();
        }
        catch (DomainException ex)
        {
            return UnprocessableEntity(new { error = ex.Message });
        }
    }

    [HttpPost("{id:guid}/cancel")]
    public async Task<IActionResult> Cancel(Guid id, CancellationToken ct)
    {
        var order = await _db.Orders
            .FirstOrDefaultAsync(x => x.Id == id, ct);

        if (order is null)
            return NotFound();

        order.Cancel();
        await _db.SaveChangesAsync(ct);
        return NoContent();
    }

    private static OrderResponse MapToResponse(Order order) => new(
        order.Id,
        order.CustomerId,
        order.Status.ToString(),
        order.Lines.Select(l => new OrderLineResponse(l.Id, l.Sku, l.Description, l.Quantity, l.UnitPrice)).ToList()
    );
}

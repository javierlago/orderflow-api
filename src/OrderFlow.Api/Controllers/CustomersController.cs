using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderFlow.Api.Contracts.Customers;
using OrderFlow.Domain.Customers;
using OrderFlow.Infrastructure.Persistence;

namespace OrderFlow.Api.Controllers;

[ApiController]
[Route("api/customers")]
public class CustomersController : ControllerBase
{
    private readonly OrderFlowDbContext _db;

    public CustomersController(OrderFlowDbContext db)
    {
        _db = db;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateCustomerRequest request, CancellationToken ct)
    {
        var customer = new Customer(request.Name, request.TaxId, request.Email);

        _db.Customers.Add(customer);
        await _db.SaveChangesAsync(ct);

        var response = new CustomerResponse(
            customer.Id,
            customer.Name,
            customer.TaxId,
            customer.Email
        );

        return CreatedAtAction(nameof(GetById), new { id = customer.Id }, response);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CustomerResponse>> GetById(Guid id, CancellationToken ct)
    {
        var customer = await _db.Customers.AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, ct);

        if (customer is null)
            return NotFound();

        return new CustomerResponse(
            customer.Id,
            customer.Name,
            customer.TaxId,
            customer.Email
        );
    }

    [HttpGet]
    public async Task<ActionResult<List<CustomerResponse>>> List(CancellationToken ct)
    {
        var customers = await _db.Customers.AsNoTracking()
            .OrderBy(x => x.Name)
            .Select(x => new CustomerResponse(
                x.Id,
                x.Name,
                x.TaxId,
                x.Email
            ))
            .ToListAsync(ct);

        return customers;
    }
}


using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderFlow.Api.Contracts.Products;
using OrderFlow.Domain.Products;
using OrderFlow.Infrastructure.Persistence;

namespace OrderFlow.Api.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    private readonly OrderFlowDbContext _db;

    public ProductsController(OrderFlowDbContext db)
    {
        _db = db;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateProductRequest request, CancellationToken ct)
    {
        var sku = request.Sku.Trim().ToUpperInvariant();

        var exists = await _db.Products.AnyAsync(x => x.Sku == sku, ct);
        if (exists)
            return Conflict(new { message = $"SKU '{sku}' already exists." });

        var product = new Product(sku, request.Name, request.UnitPrice, request.VatRate);

        _db.Products.Add(product);
        await _db.SaveChangesAsync(ct);

        var response = new ProductResponse(
            product.Id,
            product.Sku,
            product.Name,
            product.UnitPrice,
            product.VatRate,
            product.IsActive
        );

        return CreatedAtAction(nameof(GetById), new { id = product.Id }, response);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ProductResponse>> GetById(Guid id, CancellationToken ct)
    {
        var product = await _db.Products.AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, ct);

        if (product is null)
            return NotFound();

        return new ProductResponse(
            product.Id,
            product.Sku,
            product.Name,
            product.UnitPrice,
            product.VatRate,
            product.IsActive
        );
    }

    [HttpGet]
    public async Task<ActionResult<List<ProductResponse>>> List(CancellationToken ct)
    {
        var products = await _db.Products.AsNoTracking()
            .OrderBy(x => x.Sku)
            .Select(x => new ProductResponse(
                x.Id,
                x.Sku,
                x.Name,
                x.UnitPrice,
                x.VatRate,
                x.IsActive
            ))
            .ToListAsync(ct);

        return products;
    }
}

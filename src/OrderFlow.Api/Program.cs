using FluentValidation;
using FluentValidation.AspNetCore;
using OrderFlow.Api.Validation;
using Microsoft.EntityFrameworkCore;
using OrderFlow.Infrastructure.Persistence;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CreateCustomerRequestValidator>();
builder.Services.AddDbContext<OrderFlowDbContext>(options =>
{
    var cs = builder.Configuration.GetConnectionString("OrderFlowDb");
    options.UseSqlServer(cs);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

// Health
app.MapGet("/health", () => Results.Ok(new
{
    status = "ok",
    service = "OrderFlow.Api",
    utcTime = DateTime.UtcNow,
    version = typeof(Program).Assembly.GetName().Version?.ToString() ?? "unknown"
}));

app.Run();

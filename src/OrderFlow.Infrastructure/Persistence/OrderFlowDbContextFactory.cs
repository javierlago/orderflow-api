using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace OrderFlow.Infrastructure.Persistence;

public class OrderFlowDbContextFactory : IDesignTimeDbContextFactory<OrderFlowDbContext>
{
    public OrderFlowDbContext CreateDbContext(string[] args)
    {
        var currentDir = Directory.GetCurrentDirectory();

        // Si ya estamos dentro de src/OrderFlow.Api, úsalo. Si no, construye la ruta desde la raíz.
        var apiPath = currentDir.EndsWith(Path.Combine("src", "OrderFlow.Api"), StringComparison.OrdinalIgnoreCase)
            ? currentDir
            : Path.Combine(currentDir, "src", "OrderFlow.Api");

        var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

        var configuration = new ConfigurationBuilder()
            .SetBasePath(apiPath)
            .AddJsonFile("appsettings.json", optional: true)
            .AddJsonFile($"appsettings.{env}.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        var connectionString = configuration.GetConnectionString("OrderFlowDb");
        if (string.IsNullOrWhiteSpace(connectionString))
            throw new InvalidOperationException("Connection string 'OrderFlowDb' not found.");

        var optionsBuilder = new DbContextOptionsBuilder<OrderFlowDbContext>();
        optionsBuilder.UseSqlServer(connectionString);

        return new OrderFlowDbContext(optionsBuilder.Options);
    }
}

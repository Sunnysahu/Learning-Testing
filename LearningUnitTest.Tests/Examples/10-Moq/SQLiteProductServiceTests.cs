using LearningUnitTest.Examples.Moq10.Services;
using LearningUnitTest.Examples.Moq10.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using LearningUnitTest.Examples._10_Moq.Models;

namespace LearningUnitTest.Tests.Examples.Moq;

public class SQLiteProductServiceTests
{
    [Fact]
    public async Task GetProductByIdAsync_ShouldReturnProduct_WhenProductExists()
    {
        // Arrange: Create and open an in-memory SQLite connection.
        await using var connection = new SqliteConnection("Data Source=:memory:");

        await connection.OpenAsync();

        var options = new DbContextOptionsBuilder<ProductDbContext>()
            .UseSqlite(connection)
            .Options;

        await using var context = new ProductDbContext(options);

        // Create the database schema.
        await context.Database.EnsureCreatedAsync();

        // Insert test data.
        context.Products.Add(new Product
        {
            Id = 1,
            Name = "Laptop",
            Price = 50000
        });

        await context.SaveChangesAsync();

        var productService = new ProductService(context);

        // Act
        var result = await productService.GetProductByIdAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Laptop", result.Name);
        Assert.Equal(50000, result.Price);
    }

    [Fact]
    public async Task GetProductByIdAsync_ShouldReturnNull_WhenProductDoesNotExist()
    {
        // Arrange
        await using var connection = new SqliteConnection("Data Source=:memory:");

        await connection.OpenAsync();

        var options = new DbContextOptionsBuilder<ProductDbContext>()
            .UseSqlite(connection)
            .Options;

        await using var context = new ProductDbContext(options);

        await context.Database.EnsureCreatedAsync();

        var productService = new ProductService(context);

        // Act
        var result = await productService.GetProductByIdAsync(999);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task AddProductAsync_ShouldSaveProduct_WhenValid()
    {
        // Arrange
        await using var connection =
            new SqliteConnection("Data Source=:memory:");

        await connection.OpenAsync();

        var options = new DbContextOptionsBuilder<ProductDbContext>()
            .UseSqlite(connection)
            .Options;

        await using var context = new ProductDbContext(options);

        await context.Database.EnsureCreatedAsync();

        var productService = new ProductService(context);

        var product = new Product
        {
            Name = "Keyboard",
            Price = 1500
        };

        // Act
        var result = await productService.AddProductAsync(product);

        // Assert
        Assert.NotEqual(0, result.Id);
        Assert.Equal("Keyboard", result.Name);
        Assert.Equal(1500, result.Price);

        var savedProduct = await context.Products.FirstOrDefaultAsync(x => x.Id == result.Id);

        Assert.NotNull(savedProduct);
        Assert.Equal("Keyboard", savedProduct.Name);
    }
}

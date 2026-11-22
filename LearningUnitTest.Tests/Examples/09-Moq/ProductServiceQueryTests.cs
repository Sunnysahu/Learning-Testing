using LearningUnitTest.Examples.Moq09.Data;
using LearningUnitTest.Examples.Moq09.Models;
using LearningUnitTest.Examples.Moq09.Services;
using Microsoft.EntityFrameworkCore;

namespace LearningUnitTest.Tests.Examples.Moq;

public class ProductServiceQueryTests
{
    [Fact]
    public async Task GetProductByIdAsync_ShouldReturnProduct_WhenProductExists()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ProductDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        await using var context = new ProductDbContext(options);

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
}
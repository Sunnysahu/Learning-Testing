using LearningUnitTest.Examples.Moq09.Data;
using LearningUnitTest.Examples.Moq09.Models;
using LearningUnitTest.Examples.Moq09.Services;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace LearningUnitTest.Tests.Examples.Moq;

public class ProductServiceTests
{
    private readonly Mock<ProductDbContext> _mockContext;
    private readonly Mock<DbSet<Product>> _mockProducts;
    private readonly ProductService _productService;

    public ProductServiceTests()
    {
        var options = new DbContextOptions<ProductDbContext>();

        _mockContext = new Mock<ProductDbContext>(options);
        _mockProducts = new Mock<DbSet<Product>>();

        _mockContext
            .Setup(x => x.Products)
            .Returns(_mockProducts.Object);

        _productService = new ProductService(_mockContext.Object);
    }

    [Fact]
    public async Task AddProductAsync_ShouldSaveProduct()
    {
        // Arrange
        var product = new Product
        {
            Id = 1,
            Name = "Laptop",
            Price = 50000
        };

        // Act
        var result = await _productService.AddProductAsync(product);

        // Assert
        Assert.Equal("Laptop", result.Name);

        _mockProducts.Verify(
            x => x.Add(product),
            Times.Once);

        _mockContext.Verify(
            x => x.SaveChangesAsync(It.IsAny<CancellationToken>()),
            Times.Once);
    }
}

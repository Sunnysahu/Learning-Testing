using LearningUnitTest.Examples.AsyncMoq;
using LearningUnitTest.Models;
using Moq;
using System.ComponentModel;

namespace LearningUnitTest.Tests.Examples.AsyncMoq;

public class AsyncUserServiceTests
{
    private readonly Mock<IAsyncUserStore> _mockStore;
    private readonly AsyncUserService _userService;

    public AsyncUserServiceTests()
    {
        _mockStore = new Mock<IAsyncUserStore>();
        _userService = new AsyncUserService(_mockStore.Object);
    }

    [Fact]
    public async Task RegisterUserAsync_ShouldReturnUser_WhenValid()
    {
        // Arrange
        _mockStore
            .Setup(x => x.GetByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync((User?) null);

        _mockStore
            .Setup(x => x.AddAsync(It.IsAny<User>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _userService.RegisterUserAsync("Sunny", "sunny@gmail.com");

        // Assert
        Assert.Equal("Sunny", result.Name);
        Assert.Equal("sunny@gmail.com", result.Email);

        _mockStore.Verify(x => x.AddAsync(It.IsAny<User>()),Times.Once);
    }

    [Fact]
    public async Task RegisterUserAsync_ShouldThrowException_WhenStoreFails()
    {
        // Arrange
        _mockStore
            .Setup(x => x.GetByEmailAsync(It.IsAny<string>()))
            .ThrowsAsync(new Exception("Database connection failed"));

        // Act & Assert
        var exception = await Assert.ThrowsAsync<Exception>(
            () => _userService.RegisterUserAsync("Sunny", "sunny@gmail.com"));

        Assert.Equal("Database connection failed", exception.Message);
    }
}
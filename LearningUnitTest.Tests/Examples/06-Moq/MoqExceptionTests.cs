using LearningUnitTest.Examples._02_Moq;
using LearningUnitTest.Models;
using Moq;

namespace LearningUnitTest.Tests.Examples.Moq;

public class MoqExceptionTests
{
    private readonly Mock<IUserStore> _mockStore;
    private readonly MoqUserService _userService;

    public MoqExceptionTests()
    {
        _mockStore = new Mock<IUserStore>();
        _userService = new MoqUserService(_mockStore.Object);
    }

    [Fact]
    public void RegisterUser_ShouldThrowException_WhenStoreFails()
    {
        // Arrange
        _mockStore
            .Setup(x => x.GetByEmail(It.IsAny<string>()))
            .Throws(new Exception("Database connection failed"));

        // Act & Assert
        var exception = Assert.Throws<Exception>(() => _userService.RegisterUser("Sunny", "sunny@gmail.com"));

        Assert.Equal("Database connection failed", exception.Message);
    }
}

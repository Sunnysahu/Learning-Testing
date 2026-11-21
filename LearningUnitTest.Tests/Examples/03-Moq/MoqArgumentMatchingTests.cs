using LearningUnitTest.Examples._02_Moq;
using LearningUnitTest.Models;
using Moq;

namespace LearningUnitTest.Tests.Examples.Moq;

public class MoqArgumentMatchingTests
{
    private readonly Mock<IUserStore> _mockStore;
    private readonly MoqUserService _userService;

    public MoqArgumentMatchingTests()
    {
        _mockStore = new Mock<IUserStore>();
        _userService = new MoqUserService(_mockStore.Object);
    }

    [Fact]
    public void RegisterUser_ShouldAddUser_WithCorrectName()
    {
        // Arrange
        _mockStore
            .Setup(x => x.GetByEmail(It.IsAny<string>()))
            .Returns((User?) null);

        // Act
        _userService.RegisterUser("Sunny", "sunny@gmail.com");

        // Assert
        _mockStore.Verify(x => x.Add(It.Is<User>(u => u.Name == "Sunny")),Times.Once);
    }
}
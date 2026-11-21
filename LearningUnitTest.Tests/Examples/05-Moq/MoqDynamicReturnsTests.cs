using LearningUnitTest.Examples._02_Moq;
using LearningUnitTest.Models;
using Moq;

namespace LearningUnitTest.Tests.Examples.Moq;

public class MoqDynamicReturnsTests
{
    private readonly Mock<IUserStore> _mockStore;
    private readonly MoqUserService _userService;

    public MoqDynamicReturnsTests()
    {
        _mockStore = new Mock<IUserStore>();
        _userService = new MoqUserService(_mockStore.Object);
    }

    [Fact]
    public void RegisterUser_ShouldThrowException_WhenEmailAlreadyExists()
    {
        // Arrange
        _mockStore
            .Setup(x => x.GetByEmail(It.IsAny<string>()))
            .Returns((string email) => new User
            {
                Id = 1,
                Name = "Existing User",
                Email = email
            });

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => _userService.RegisterUser("Sunny", "sunny@gmail.com"));
    }
}

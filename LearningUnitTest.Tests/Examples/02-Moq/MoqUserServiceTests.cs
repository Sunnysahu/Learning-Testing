using LearningUnitTest.Examples._02_Moq;
using LearningUnitTest.Models;
using Moq;

namespace LearningUnitTest.Tests.Examples.Moq;

public class MoqUserServiceTests
{
    private readonly Mock<IUserStore> _mockStore;
    private readonly MoqUserService _userService;

    public MoqUserServiceTests()
    {
        _mockStore = new Mock<IUserStore>();

        _userService = new MoqUserService(_mockStore.Object);
    }

    [Fact]
    public void RegisterUser_ShouldReturnUser_WhenValid()
    {
        // Arrange
        _mockStore
            .Setup(x => x.GetByEmail("sunny@gmail.com"))
            .Returns((User?) null);

        // Act
        var result = _userService.RegisterUser("Sunny", "sunny@gmail.com");

        // Assert
        Assert.Equal("Sunny", result.Name);
        Assert.Equal("sunny@gmail.com", result.Email);

        _mockStore.Verify(x => x.Add(It.IsAny<User>()), Times.Once);
    }

    [Fact]
    public void RegisterUser_ShouldThrowException_WhenEmailExists()
    {
        // Arrange
        _mockStore
            .Setup(x => x.GetByEmail("sunny@gmail.com"))
            .Returns(new User
            {
                Id = 1,
                Name = "Sunny",
                Email = "sunny@gmail.com"
            });

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => _userService.RegisterUser("Rahul", "sunny@gmail.com"));

        _mockStore.Verify(x => x.Add(It.IsAny<User>()), Times.Never);
    }
}
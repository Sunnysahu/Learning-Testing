
using LearningUnitTest.Examples._02_Moq;
using LearningUnitTest.Models;
using Moq;

namespace LearningUnitTest.Tests.Examples.Moq;

public class MoqCallbackTests
{
    private readonly Mock<IUserStore> _mockStore;
    private readonly MoqUserService _userService;

    public MoqCallbackTests()
    {
        _mockStore = new Mock<IUserStore>();
        _userService = new MoqUserService(_mockStore.Object);
    }

    [Fact]
    public void RegisterUser_ShouldCaptureUser_WhenValid()
    {
        // Arrange
        User? capturedUser = null;

        _mockStore
            .Setup(x => x.GetByEmail(It.IsAny<string>()))
            .Returns((User?) null);

        _mockStore
            .Setup(x => x.Add(It.IsAny<User>()))
            .Callback<User>(user => capturedUser = user);

        // Act
        _userService.RegisterUser("Sunny", "sunny@gmail.com");

        // Assert
        Assert.NotNull(capturedUser);
        Assert.Equal("Sunny", capturedUser.Name);
        Assert.Equal("sunny@gmail.com", capturedUser.Email);
    }
}

/*
[Fact]
public void RegisterUser_ShouldAddUser_WhenValid()
{
    // Arrange
    User? capturedUser = null;

    _mockStore
        .Setup(x => x.GetByEmail(It.IsAny<string>()))
        .Returns((User?)null);

    _mockStore
        .Setup(x => x.Add(It.IsAny<User>()))
        .Callback<User>(user => capturedUser = user);

    // Act
    _userService.RegisterUser("Sunny", "sunny@gmail.com");

    // Assert

    // Verify method interaction
    _mockStore.Verify(
        x => x.Add(It.IsAny<User>()),
        Times.Once);

    // Verify captured object
    Assert.NotNull(capturedUser);
    Assert.Equal("Sunny", capturedUser.Name);
    Assert.Equal("sunny@gmail.com", capturedUser.Email);
}
*/
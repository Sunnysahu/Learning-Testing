using LearningUnitTest.Examples._02_Moq;
using LearningUnitTest.Models;
using Moq;

namespace LearningUnitTest.Tests.Examples.Moq;

public class MoqSetupSequenceTests
{
    private readonly Mock<IUserStore> _mockStore;
    private readonly MoqUserService _userService;

    public MoqSetupSequenceTests()
    {
        _mockStore = new Mock<IUserStore>();
        _userService = new MoqUserService(_mockStore.Object);
    }

    [Fact]
    public void GetByEmail_ShouldReturnDifferentResults_OnEachCall()
    {
        // Arrange
        _mockStore
            .SetupSequence(x => x.GetByEmail(It.IsAny<string>()))
            .Returns((User?) null)
            .Returns(new User
            {
                Id = 1,
                Name = "Existing User",
                Email = "sunny@gmail.com"
            });

        // Act
        var firstResult = _mockStore.Object.GetByEmail("sunny@gmail.com");
        var secondResult = _mockStore.Object.GetByEmail("sunny@gmail.com");

        // Assert
        Assert.Null(firstResult);
        Assert.NotNull(secondResult);
        Assert.Equal("Existing User", secondResult.Name);
    }
}

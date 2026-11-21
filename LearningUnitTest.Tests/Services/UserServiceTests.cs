
using LearningUnitTest.Services;

namespace LearningUnitTest.Tests.Services;

public class UserServiceTests
{
    private readonly UserService _userService;

    public UserServiceTests()
    {
        _userService = new UserService();
    }

    // Test 1: Successful registration
    [Fact]
    public void RegisterUser_ShouldReturnUser_WhenInputIsValid()
    {
        // Arrange
        string name = "Sunny";
        string email = "sunny@gmail.com";

        // Act
        var result = _userService.RegisterUser(name, email);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(name, result.Name);
        Assert.Equal(email, result.Email);
        Assert.Equal(1, result.Id);
    }

    // Test 2: Empty name
    [Fact]
    public void RegisterUser_ShouldThrowException_WhenNameIsEmpty()
    {
        string name = "";
        string email = "sunny@gmail.com";

        var exception = Assert.Throws<ArgumentException>(() => _userService.RegisterUser(name, email));

        Assert.Equal("Name is required.", exception.Message);
    }

    // Test 3: Empty email
    [Fact]
    public void RegisterUser_ShouldThrowException_WhenEmailIsEmpty()
    {
        string name = "Sunny";
        string email = "";


        Assert.Throws<ArgumentException>(() => _userService.RegisterUser(name, email));
    }

    // Test 4: Invalid email
    [Fact]
    public void RegisterUser_ShouldThrowException_WhenEmailIsInvalid()
    {
        string name = "Sunny";
        string email = "sunnygmail.com";

        Assert.Throws<ArgumentException>(() => _userService.RegisterUser(name, email));
    }

    // Test 5: Duplicate email
    [Fact]
    public void RegisterUser_ShouldThrowException_WhenEmailAlreadyExists()
    {
        _userService.RegisterUser("Sunny", "sunny@gmail.com");

        Assert.Throws<InvalidOperationException>(() => _userService.RegisterUser("Rahul", "sunny@gmail.com"));
    }

    // Test 6: Email comparison should ignore case
    [Fact]
    public void RegisterUser_ShouldRejectDuplicateEmail_IgnoringCase()
    {
        _userService.RegisterUser("Sunny", "sunny@gmail.com");

        Assert.Throws<InvalidOperationException>(() => _userService.RegisterUser("Rahul", "SUNNY@GMAIL.COM"));
    }

    // Test 7: Multiple users
    [Fact]
    public void GetAllUsers_ShouldReturnAllRegisteredUsers()
    {
        _userService.RegisterUser("Sunny", "sunny@gmail.com");
        _userService.RegisterUser("Rahul", "rahul@gmail.com");

        var result = _userService.GetAllUsers();

        Assert.Equal(2, result.Count);
    }
}

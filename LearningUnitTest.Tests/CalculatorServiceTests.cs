
namespace LearningUnitTest.Tests;

public class CalculatorServiceTests
{
    private readonly CalculatorService _calculator;

    public CalculatorServiceTests()
    {
        _calculator = new CalculatorService();
    }

    [Fact]
    public void Add_ShouldReturnCorrectResult()
    {
        // Arrange
        int a = 10;
        int b = 20;

        // Act
        int result = _calculator.Add(a, b);

        // Assert
        Assert.Equal(30, result);
    }


    [Theory]
    [InlineData(10, 20, 30)]
    [InlineData(5, 5, 10)]
    [InlineData(10, -5, 5)]
    [InlineData(0, 10, 10)]
    public void Add_ShouldReturnCorrectResult_ForMultipleInputs(
        int a,
        int b,
        int expected)
    {
        // Arrange

        // Act
        int result = _calculator.Add(a, b);

        // Assert
        Assert.Equal(expected, result);
    }

}

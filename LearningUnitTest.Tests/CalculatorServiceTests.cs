
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
}

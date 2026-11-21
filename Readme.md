
## 2. What is Unit Testing?

Unit testing means testing a small piece of code independently to verify that it produces the expected result.

For example, suppose you have this service:

```csharp
public class CalculatorService
{
    public int Add(int a, int b)
    {
        return a + b;
    }
}
```

You want to verify:

- `10 + 20` should return `30`.
- `5 + 5` should return `10`.
- `10 + (-5)` should return `5`.

Instead of manually checking these results every time, we write automated tests.

**That's Unit Testing!**

---

## 3. What is xUnit?

**xUnit** is a popular testing framework for .NET and C# applications.

It provides attributes and methods to write automated tests.

### Our Testing Framework: xUnit

### Important xUnit Attributes and Methods

| Attribute / Method | Purpose |
|---|---|
| `[Fact]` | Tests one specific scenario |
| `[Theory]` | Tests multiple input scenarios |
| `[InlineData]` | Supplies input values to a Theory |
| `Assert.Equal()` | Checks expected vs. actual result |
| `Assert.True()` | Checks whether a condition is true |
| `Assert.False()` | Checks whether a condition is false |
| `Assert.Throws()` | Checks whether an exception occurs |

### Other Testing Frameworks

Other popular testing frameworks available for .NET include:

- NUnit
- MSTest
- xUnit

**We will use xUnit for writing unit tests in our .NET applications.**

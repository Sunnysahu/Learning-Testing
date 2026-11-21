## What is `[Fact]`?

`[Fact]` is an xUnit attribute that tells the testing framework that a method is a test case and should be executed when running the test suite.

```csharp
[Fact]
public void Add_ShouldReturnCorrectResult()
{
    // Test logic
}
```

## What is `Assert.Equal()`?

`Assert.Equal()` compares the expected result with the actual result.

```csharp
Assert.Equal(30, result);
```

- **Expected:** `30`
- **Actual:** `result`

If both values match, the test **passes**. Otherwise, it **fails**.

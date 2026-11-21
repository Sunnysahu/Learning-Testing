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



## 6. What is `[Theory]` in xUnit?

`[Theory]` is an xUnit attribute used to test the same method with multiple input values without writing separate test methods.

### 1. Example using `[Fact]`

Suppose you want to test an addition method:

```csharp
[Fact]
public void Add_ShouldReturnCorrectResult()
{
    var result = _calculator.Add(10, 20);

    Assert.Equal(30, result);
}
```

If you want to test multiple inputs, you would need multiple `[Fact]` methods.

### 2. Example using `[Theory]`

Instead, we can use `[Theory]` with `[InlineData]`.

```csharp
[Theory]
[InlineData(10, 20, 30)]
[InlineData(5, 5, 10)]
[InlineData(10, -5, 5)]
public void Add_ShouldReturnCorrectResult(int a, int b, int expected)
{
    var result = _calculator.Add(a, b);

    Assert.Equal(expected, result);
}
```

### 3. How does it work?

`[InlineData]` supplies different input values to the same test method.

| Input A | Input B | Expected |
|---|---|---|
| 10 | 20 | 30 |
| 5 | 5 | 10 |
| 10 | -5 | 5 |

xUnit executes the same test method three times, once for each `[InlineData]`.

### 4. Difference between `[Fact]` and `[Theory]`

| `[Fact]` | `[Theory]` |
|---|---|
| Tests one specific scenario | Tests multiple scenarios |
| No input data required | Uses input data |
| Executes once | Executes once per data set |
| Uses `[Fact]` | Uses `[Theory]` and `[InlineData]` |

### 5. When should you use them?

- **`[Fact]`**: When you want to test one specific scenario.
- **`[Theory]`**: When you want to test the same logic with different input values.

**Remember:**
- `[Fact]` = Single test scenario.
- `[Theory]` = Multiple test scenarios using different data.

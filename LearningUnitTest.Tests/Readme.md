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

---

# Unit Testing with Moq – Mocking

## Overview

In unit testing, we should test business logic independently without depending on external systems such as databases.

Suppose `UserService` depends on SQL Server to check whether an email already exists before registering a user.

Normally:

UserService → Database → SQL Server

During unit testing, we don't want to connect to SQL Server.

Instead, we create a fake dependency that behaves as we configure it. This is called **Mocking**.

In this tutorial, we'll use the **Moq** library.

## 1. Install Moq

In Visual Studio:

1. Right-click `LearningUnitTest.Tests`.
2. Select **Manage NuGet Packages**.
3. Search for `Moq`.
4. Install the latest compatible stable version.

## 2. Prepare the Service for Mocking

To test `UserService` independently, it should depend on an interface rather than a concrete database implementation.

We'll introduce an `IUserStore` interface that defines two operations:

- `GetByEmail()` – Checks whether a user already exists.
- `Add()` – Saves a new user.

Update `UserService` to receive `IUserStore` through constructor dependency injection.

**Why?**

This allows us to replace the actual database dependency with a mock object during unit testing.

Our service only knows about the interface, not SQL Server.

## 3. Test Successful User Registration

**Test:** `RegisterUser_ShouldReturnUser_WhenValid`

**Scenario:** A user registers with a valid name and a new email address.

**Test steps:**

1. Configure the mock to return `null` when the email is searched.
2. Call the user registration method.
3. Verify that the returned user's name and email are correct.
4. Verify that the `Add()` method was called exactly once.

**Expected Result:** The user is successfully registered.

## 4. Understand Important Moq Methods

| Method | Purpose |
|---|---|
| `Mock<T>()` | Creates a mock object. |
| `Setup()` | Configures mock behavior. |
| `Returns()` | Defines what the mock returns. |
| `Verify()` | Checks whether a method was called. |
| `It.IsAny<T>()` | Accepts any argument of type T. |
| `Times.Once` | Expects exactly one call. |
| `Times.Never` | Expects no calls. |

### Setup()

Configures how a mocked method should behave.

For example, we configure `GetByEmail()` to return `null` to simulate a user who doesn't exist.

### Returns()

Defines the value returned by the mocked method.

### Verify()

Checks whether a particular dependency method was called.

For example, we verify that `Add()` was called exactly once after successful registration.

## 5. Test Duplicate Email Registration

**Test:** `RegisterUser_ShouldThrowException_WhenEmailAlreadyExists`

**Scenario:** A user attempts to register using an email address that already exists.

**Test steps:**

1. Create an existing user.
2. Configure the mock to return that existing user when the email is searched.
3. Attempt to register another user with the same email.
4. Verify that an `InvalidOperationException` is thrown.
5. Verify that the `Add()` method is never called.

**Expected Result:** Registration is rejected, and no new user is saved.

## 6. Run All Tests

In Visual Studio:

1. Open **Test → Test Explorer**.
2. Click **Run All Tests**.
3. Check the test results.

Both registration tests should pass.

## 7. What Did We Achieve?

We successfully tested `UserService` without connecting to SQL Server.

We verified:

- Successful user registration.
- Duplicate email rejection.
- Correct mock configuration.
- Whether dependency methods were called.
- Whether the database save operation was executed when expected.

## Key Takeaway

**Mocking allows us to test business logic independently by replacing real dependencies with controlled fake implementations.**

With Moq, we can simulate database responses, control dependency behavior, and verify interactions without requiring an actual database connection.

---
# What is `It` in xUnit Testing?

## 1. Introduction

`It` is not a built-in feature of xUnit. It is a class provided by the **Moq mocking framework**, which is commonly used alongside xUnit in .NET unit testing.

It is used to match arguments passed to mocked methods.

## 2. What is `It`?

`It` is a static class in Moq that provides methods to match method arguments.

For example:

```csharp
mock.Setup(x => x.GetUser(It.IsAny<int>()))
    .Returns(new User());
```

**Explanation:**
- `It` is a class provided by Moq.
- `IsAny<int>()` accepts any integer value.
- `GetUser()` is the mocked method.
- `Returns()` specifies what the mocked method should return.

## 3. Common Methods of `It`

### A. It.IsAny<T>()

Matches any argument of the specified type.

```csharp
mock.Setup(x => x.GetUser(It.IsAny<int>()))
    .Returns(new User());
```

Example: Accepts any integer, such as 1, 2, 100, or 500.

### B. It.Is<T>()

Matches an argument based on a specific condition.

```csharp
mock.Setup(x => x.GetUser(It.Is<int>(id => id > 0)))
    .Returns(new User());
```

Example: Matches only integer values greater than zero.

### C. It.IsIn<T>()

Matches an argument against specified values.

```csharp
mock.Setup(x => x.GetStatus(It.IsIn("Active", "Pending")))
    .Returns("Valid");
```

Example: Matches either "Active" or "Pending".

### D. It.IsNotIn<T>()

Matches an argument that is not included in specified values.

```csharp
mock.Setup(x => x.GetStatus(It.IsNotIn("Deleted", "Blocked")))
    .Returns("Allowed");
```

Example: Matches values other than "Deleted" and "Blocked".

## 4. Complete Example Using xUnit and Moq

### Interface

```csharp
public interface IUserService
{
    User GetUser(int id);
}
```

### User Class

```csharp
public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
}
```

### Unit Test

```csharp
using Moq;
using Xunit;

public class UserServiceTests
{
    [Fact]
    public void GetUser_ShouldReturnUser_WhenUserExists()
    {
        // Arrange
        var mock = new Mock<IUserService>();

        mock.Setup(x => x.GetUser(It.IsAny<int>()))
            .Returns(new User
            {
                Id = 1,
                Name = "John"
            });

        // Act
        var result = mock.Object.GetUser(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("John", result.Name);
    }
}
```

## 5. Difference Between xUnit and Moq

| Feature | xUnit | Moq |
|---|---|---|
| Unit testing | Yes | No |
| Mock objects | No | Yes |
| `[Fact]` | Yes | No |
| `[Theory]` | Yes | No |
| `Assert.Equal()` | Yes | No |
| `It.IsAny<T>()` | No | Yes |
| `It.Is<T>()` | No | Yes |
| `Mock<T>()` | No | Yes |

## 6. Conclusion

Remember:

- **xUnit:** Used to write and execute unit tests.
- **Moq:** Used to create mock objects.
- **It:** Used to match arguments when setting up or verifying mocked method calls.

The most commonly used methods are:

| Method | Purpose |
|---|---|
| `It.IsAny<T>()` | Matches any value of a specified type |
| `It.Is<T>()` | Matches values satisfying a condition |
| `It.IsIn<T>()` | Matches values from a specified collection |
| `It.IsNotIn<T>()` | Matches values outside a specified collection |

---

# Callback() in Moq

### What is Callback()?

`Callback()` is a method provided by the Moq framework. It lets us execute our own custom code when a mocked method is called.

We commonly use `Callback()` to:
- Capture method arguments.
- Store values passed to a mocked method.
- Execute custom logic when a method is called.
- Track whether a method was called and with which values.

### Syntax

```csharp
mock.Setup(x => x.Save(It.IsAny<string>()))
    .Callback<string>(value =>
    {
        // Execute custom code here
    });
```

### Example

```csharp
public interface IUserService
{
    void SaveUser(string name);
}
```

**Unit Test:**

```csharp
[Fact]
public void SaveUser_ShouldCaptureUserName()
{
    // Arrange
    var mock = new Mock<IUserService>();

    string capturedName = null;

    mock.Setup(x => x.SaveUser(It.IsAny<string>()))
        .Callback<string>(name =>
        {
            capturedName = name;
        });

    // Act
    mock.Object.SaveUser("John");

    // Assert
    Assert.Equal("John", capturedName);
}
```

### Explanation

1. `Setup()` configures the mocked method.
2. `It.IsAny<string>()` accepts any string argument.
3. `Callback<string>()` executes custom code when the method is called.
4. `name` receives the argument passed to `SaveUser()`.
5. `capturedName = name` stores the received value.
6. `Assert.Equal()` verifies that the expected value was captured.

### Important Point

`Callback()` does not define the return value of a mocked method. It executes custom code when the configured method is called.

Use `Returns()` when you want to specify a return value.

### Difference Between Returns() and Callback()

| Method | Purpose |
|---|---|
| `Returns()` | Specifies the return value of a mocked method. |
| `Callback()` | Executes custom code when a mocked method is called. |

**Remember:**

`Callback()` = Execute our own code when a mocked method is called.

---
# SetupSequence() in Moq

## 1. What is SetupSequence()?

`SetupSequence()` is a method provided by the Moq framework.

It allows us to configure a mocked method to return different values or throw different exceptions on successive calls.

In simple words:

**SetupSequence() lets us define a sequence of behaviors for a mocked method whenever it is called multiple times.**

## 2. Syntax

```csharp
mock.SetupSequence(x => x.GetStatus())
    .Returns("Pending")
    .Returns("Processing")
    .Returns("Completed");
```

In this example:
- First call returns `"Pending"`.
- Second call returns `"Processing"`.
- Third call returns `"Completed"`.

## 3. Practical Example

### Interface

```csharp
public interface IOrderService
{
    string GetOrderStatus();
}
```

### Unit Test Using xUnit and Moq

```csharp
[Fact]
public void GetOrderStatus_ShouldReturnDifferentStatuses()
{
    // Arrange
    var mock = new Mock<IOrderService>();

    mock.SetupSequence(x => x.GetOrderStatus())
        .Returns("Pending")
        .Returns("Processing")
        .Returns("Completed");

    // Act
    var firstCall = mock.Object.GetOrderStatus();
    var secondCall = mock.Object.GetOrderStatus();
    var thirdCall = mock.Object.GetOrderStatus();

    // Assert
    Assert.Equal("Pending", firstCall);
    Assert.Equal("Processing", secondCall);
    Assert.Equal("Completed", thirdCall);
}
```

## 4. SetupSequence() with Exceptions

We can also configure a mocked method to throw an exception during a particular call.

```csharp
mock.SetupSequence(x => x.GetOrderStatus())
    .Returns("Pending")
    .Throws(new Exception("Something went wrong"))
    .Returns("Completed");
```

In this example:
- First call returns `"Pending"`.
- Second call throws an exception.
- Third call returns `"Completed"`.

## 5. Difference Between Setup() and SetupSequence()

| Setup() | SetupSequence() |
|---|---|
| Configures a mocked method with a behavior. | Configures multiple successive behaviors. |
| Generally returns the configured value on every call. | Returns different configured values on successive calls. |
| Useful for a single consistent behavior. | Useful for testing changing behavior across multiple calls. |

### Example: Setup()

```csharp
mock.Setup(x => x.GetOrderStatus())
    .Returns("Pending");
```

Every call returns `"Pending"`.

### Example: SetupSequence()

```csharp
mock.SetupSequence(x => x.GetOrderStatus())
    .Returns("Pending")
    .Returns("Processing")
    .Returns("Completed");
```

Each successive call returns the next configured value.

## 6. When Should We Use SetupSequence()?

Use `SetupSequence()` when you want to test:

- Retry mechanisms.
- API calls that return different responses.
- Changing order or payment statuses.
- Temporary failures followed by successful responses.
- Methods that behave differently on successive calls.

## 7. Important Points to Remember

- `SetupSequence()` belongs to Moq, not xUnit.
- It configures different behaviors for successive method calls.
- It supports both `Returns()` and `Throws()`.
- It is particularly useful when testing retry logic and multiple execution scenarios.

**Remember:**

`Setup()` = Configure a consistent behavior.

`SetupSequence()` = Configure different behaviors for successive calls.


---

# Async Moq — ReturnsAsync() and ThrowsAsync()

## 1. What is Async Moq?

When working with asynchronous methods in C#, we commonly use `async` and `await` with methods that return `Task` or `Task<T>`.

In unit testing, Moq provides methods such as:

- `ReturnsAsync()` – Configures a mocked asynchronous method to return a successful result.
- `ThrowsAsync()` – Configures a mocked asynchronous method to throw an exception.

These methods are useful when testing asynchronous code using xUnit and Moq.

## 2. ReturnsAsync()

### What is ReturnsAsync()?

`ReturnsAsync()` is a Moq method used to configure a mocked asynchronous method to return a specific result wrapped in a completed `Task`.

In simple words:

**ReturnsAsync() lets us specify what value an asynchronous mocked method should return.**

### Example

**Interface:**

```csharp
public interface IUserService
{
    Task<User> GetUserAsync(int id);
}
```

**User Class:**

```csharp
public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
}
```

**Unit Test:**

```csharp
[Fact]
public async Task GetUserAsync_ShouldReturnUser()
{
    // Arrange
    var mock = new Mock<IUserService>();

    mock.Setup(x => x.GetUserAsync(It.IsAny<int>()))
        .ReturnsAsync(new User
        {
            Id = 1,
            Name = "John"
        });

    // Act
    var result = await mock.Object.GetUserAsync(1);

    // Assert
    Assert.NotNull(result);
    Assert.Equal("John", result.Name);
}
```

### Explanation

1. `Setup()` configures the mocked asynchronous method.
2. `It.IsAny<int>()` accepts any integer argument.
3. `ReturnsAsync()` specifies the result returned by the asynchronous method.
4. `await` waits for the asynchronous operation to complete.
5. `Assert.Equal()` verifies the expected result.

## 3. ThrowsAsync()

### What is ThrowsAsync()?

`ThrowsAsync()` is a Moq method used to configure a mocked asynchronous method to throw an exception when it is awaited.

In simple words:

**ThrowsAsync() lets us simulate an exception from an asynchronous mocked method.**

### Example

```csharp
[Fact]
public async Task GetUserAsync_ShouldThrowException()
{
    // Arrange
    var mock = new Mock<IUserService>();

    mock.Setup(x => x.GetUserAsync(It.IsAny<int>()))
        .ThrowsAsync(new Exception("User not found"));

    // Act & Assert
    var exception = await Assert.ThrowsAsync<Exception>(
        () => mock.Object.GetUserAsync(1)
    );

    Assert.Equal("User not found", exception.Message);
}
```

### Explanation

1. `Setup()` configures the mocked method.
2. `It.IsAny<int>()` accepts any integer argument.
3. `ThrowsAsync()` configures the mocked method to throw an exception.
4. `Assert.ThrowsAsync()` verifies that the expected exception is thrown.
5. `Assert.Equal()` checks the exception message.

## 4. Difference Between ReturnsAsync() and ThrowsAsync()

| ReturnsAsync() | ThrowsAsync() |
|---|---|
| Returns a successful asynchronous result. | Throws an asynchronous exception. |
| Used to test successful scenarios. | Used to test failure scenarios. |
| Simulates successful API or database responses. | Simulates API or database failures. |

## 5. Real-World Example: API Success and Failure

Suppose we have an API that retrieves user information.

### Successful Scenario

```csharp
mock.Setup(x => x.GetUserAsync(1))
    .ReturnsAsync(new User
    {
        Id = 1,
        Name = "John"
    });
```

The mocked API returns user information successfully.

### Failure Scenario

```csharp
mock.Setup(x => x.GetUserAsync(1))
    .ThrowsAsync(new Exception("API is unavailable"));
```

The mocked API throws an exception.

## 6. Difference Between Returns() and ReturnsAsync()

| Returns() | ReturnsAsync() |
|---|---|
| Used for synchronous methods. | Used for asynchronous methods. |
| Returns a direct value. | Returns a value wrapped in a completed Task. |
| Example: `string` | Example: `Task<string>` |

### Returns() Example

```csharp
mock.Setup(x => x.GetName())
    .Returns("John");
```

### ReturnsAsync() Example

```csharp
mock.Setup(x => x.GetNameAsync())
    .ReturnsAsync("John");
```

## 7. Important Points to Remember

- `ReturnsAsync()` and `ThrowsAsync()` belong to Moq, not xUnit.
- `ReturnsAsync()` is used to simulate successful asynchronous operations.
- `ThrowsAsync()` is used to simulate failed asynchronous operations.
- Use `await` when calling asynchronous methods in your tests.
- Use `Assert.ThrowsAsync()` to verify exceptions from asynchronous methods.

**Remember:**

- `ReturnsAsync()` = Return an asynchronous result.
- `ThrowsAsync()` = Throw an exception from an asynchronous operation.

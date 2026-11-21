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

---------------------------------------------------------------------

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
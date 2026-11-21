using LearningUnitTest.Models;

namespace LearningUnitTest.Examples.AsyncMoq;

public class AsyncUserService(IAsyncUserStore userStore)
{
    public async Task<User> RegisterUserAsync(string name, string email)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name is required.");

        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email is required.");

        if (!email.Contains('@'))
            throw new ArgumentException("Invalid email address.");

        var existingUser = await userStore.GetByEmailAsync(email);

        if (existingUser != null)
            throw new InvalidOperationException("Email already exists.");

        var user = new User
        {
            Name = name,
            Email = email
        };

        await userStore.AddAsync(user);

        return user;
    }
}
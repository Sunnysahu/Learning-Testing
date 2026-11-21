using LearningUnitTest.Examples._02_Moq;
using LearningUnitTest.Models;

public class MoqUserService(IUserStore userStore)
{
    public User RegisterUser(string name, string email)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name is required.");

        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email is required.");

        if (!email.Contains('@'))
            throw new ArgumentException("Invalid email address.");

        var existingUser = userStore.GetByEmail(email);

        if (existingUser != null)
            throw new InvalidOperationException("Email already exists.");

        var user = new User
        {
            Name = name,
            Email = email
        };

        userStore.Add(user);

        return user;
    }
}
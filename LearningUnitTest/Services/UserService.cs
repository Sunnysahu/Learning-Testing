using LearningUnitTest.Models;

namespace LearningUnitTest.Services;

public class UserService
{
    private readonly List<User> _users = new();

    public User RegisterUser(string name, string email)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name is required.");

        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email is required.");

        if (!email.Contains('@'))
            throw new ArgumentException("Invalid email address.");

        if (_users.Any(x =>
            x.Email.Equals(email, StringComparison.OrdinalIgnoreCase)))
        {
            throw new InvalidOperationException("Email already exists.");
        }

        var user = new User
        {
            Id = _users.Count + 1,
            Name = name,
            Email = email
        };

        _users.Add(user);

        return user;
    }

    public List<User> GetAllUsers() => _users;
}
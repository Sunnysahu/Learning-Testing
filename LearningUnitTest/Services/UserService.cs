
using LearningUnitTest.Models;
using System.Reflection.Metadata.Ecma335;

namespace LearningUnitTest.Services;

public class UserService
{
    private readonly List<User> _users = [];

    public User RegisterUser(string name, string email)
    {
        // Validate name
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name is required.");


        // Validate email
        if (string.IsNullOrWhiteSpace(email) || !email.Contains('@'))
            throw new ArgumentException("Email is required.");


        if (!email.Contains('@'))
            throw new ArgumentException("Invalid email address.");
        
        // Check duplicate email
        bool emailExists = _users.Any(
            u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

        if (emailExists)
            throw new InvalidOperationException("Email already exists.");

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

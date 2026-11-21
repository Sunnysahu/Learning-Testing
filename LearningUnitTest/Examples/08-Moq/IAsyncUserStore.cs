using LearningUnitTest.Models;

namespace LearningUnitTest.Examples.AsyncMoq;

public interface IAsyncUserStore
{
    Task<User?> GetByEmailAsync(string email);

    Task AddAsync(User user);
}
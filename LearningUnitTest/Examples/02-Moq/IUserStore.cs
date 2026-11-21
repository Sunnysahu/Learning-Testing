using LearningUnitTest.Models;

namespace LearningUnitTest.Examples._02_Moq;

public interface IUserStore
{
    User? GetByEmail(string email);
    void Add(User user);
}
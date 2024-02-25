using RedTest.Shared.Entities;

namespace RedTest.Shared.Repositories
{
    public interface IUserRepository
    {
        Task<Result<User>> Create(User user);
        Task<Result<User>> FindBy(int userId);
        Task<bool> IsUserExist(string name);
        Task<bool> IsUserExist(int id);
        Task<Result<User>> Update(User user);
    }
}

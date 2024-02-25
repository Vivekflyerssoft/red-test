using RedTest.Shared.DTOs;
using RedTest.Shared.Entities;

namespace RedTest.Shared.Services
{
    public interface ITopUpService
    {
        IEnumerable<uint> GetAvailableTopUpOptions();
        Task<IEnumerable<Result>> TopUp(int userId, IEnumerable<TopUpDTO> topUps);
    }
}
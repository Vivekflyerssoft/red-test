using RedTest.Shared.Entities;

namespace RedTest.Shared.Repositories
{
    public interface ITopUpRepository
    {
        Task<bool> Add(TopUp topUp);
        Task<bool> TopUp(User user, IEnumerable<TopUp> topUpList);
        Task<IEnumerable<TopUp>> TopUpsForCurrentMonth(int beneficiaryId);
    }
}
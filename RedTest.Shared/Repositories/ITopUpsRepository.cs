using RedTest.Shared.Entities;

namespace RedTest.Shared.Repositories
{
    public interface ITopUpsRepository
    {
        Task<bool> Add(TopUp topUp);
        Task<IEnumerable<TopUp>> TopUpsForCurrentMonth(int beneficiaryId);
    }
}
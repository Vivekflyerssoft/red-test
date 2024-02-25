using RedTest.Shared.Entities;

namespace RedTest.Shared.Repositories
{
    public interface IBeneficiariesRepository
    {
        Task<bool> Add(Beneficiary beneficiary);
        Task<IEnumerable<Beneficiary>> GetAllBeneficiariesFor(int userId);
    }
}
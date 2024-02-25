using RedTest.Shared.Entities;

namespace RedTest.Shared.Repositories
{
    public interface IBeneficiaryRepository
    {
        Task<Result<Beneficiary>> Add(Beneficiary beneficiary);
        Task<IEnumerable<Beneficiary>> GetAllBeneficiariesFor(int userId);
    }
}
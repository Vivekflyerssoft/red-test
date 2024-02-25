using RedTest.Shared.DTOs;

namespace RedTest.Shared.Services
{
    public interface IBeneficiaryService
    {
        Task<Result<BeneficiaryDTO>> Create(int userId, BeneficiaryDTO beneficiary);
        Task<Result<IEnumerable<BeneficiaryDTO>>> GetBeneficiaries(int userId);
    }
}
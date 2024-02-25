using RedTest.Domain.Models;
using RedTest.Shared;
using RedTest.Shared.DTOs;
using RedTest.Shared.Repositories;
using RedTest.Shared.Services;

namespace RedTest.Domain.Services
{
    public class BeneficiaryService : IBeneficiaryService
    {
        private readonly IBeneficiaryRepository _beneficiariesRepository;
        private readonly IUserRepository _userRepository;

        public BeneficiaryService(IBeneficiaryRepository beneficiariesRepository, IUserRepository userRepository)
        {
            _beneficiariesRepository = beneficiariesRepository ?? throw new ArgumentNullException(nameof(beneficiariesRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<Result<IEnumerable<BeneficiaryDTO>>> GetBeneficiaries(int userId)
        {
            if (!await _userRepository.IsUserExist(userId))
            {
                return ResultFactory.Error<IEnumerable<BeneficiaryDTO>>("User not found.");
            }
            var domainModel = await _beneficiariesRepository.GetAllBeneficiariesFor(userId);
            return ResultFactory.Success(domainModel.Select(dm => new BeneficiaryDTO { NickName = dm.NickName, Id = dm.Id, UserId = dm.UserId }));
        }

        public async Task<Result<BeneficiaryDTO>> Create(int userId, BeneficiaryDTO beneficiary)
        {
            var beneficiaries = await _beneficiariesRepository.GetAllBeneficiariesFor(userId);
            Beneficiaries beneficiariesDM = new(
                beneficiaries?.Select(e => new Beneficiary(e.NickName) { Id = e.Id })?.ToList() ?? new List<Beneficiary>()
            );

            var result = beneficiariesDM.Add(new Beneficiary(beneficiary.NickName));
            if (!result.IsSuccess)
            {
                return ResultFactory.Error<BeneficiaryDTO>(result.ErrorMessage);
            }
            var dbReponse = await _beneficiariesRepository.Add(new Shared.Entities.Beneficiary { NickName = beneficiary.NickName, UserId = userId });

            beneficiary.Id = dbReponse.Data.Id;

            return dbReponse.IsSuccess ? ResultFactory.Success(beneficiary) : ResultFactory.Error<BeneficiaryDTO>("Beneficiary save failed.");
        }
    }
}

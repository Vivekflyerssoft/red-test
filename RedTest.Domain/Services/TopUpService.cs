using RedTest.Domain.Models;
using RedTest.Shared;
using RedTest.Shared.DTOs;
using RedTest.Shared.Repositories;
using RedTest.Shared.Services;

namespace RedTest.Domain.Services
{
    public class TopUpService : ITopUpService
    {
        private readonly ITopUpRepository _topUpRepository;
        private readonly IUserRepository _userRepository;
        private readonly IBeneficiaryRepository _beneficiaryRepository;

        public TopUpService(ITopUpRepository topUpRepository, IUserRepository userRepository, IBeneficiaryRepository beneficiaryRepository)
        {
            _topUpRepository = topUpRepository ?? throw new ArgumentNullException(nameof(topUpRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _beneficiaryRepository = beneficiaryRepository ?? throw new ArgumentNullException(nameof(beneficiaryRepository));
        }
        public IEnumerable<uint> GetAvailableTopUpOptions()
        {
            return new List<uint> { 5, 10, 20, 30, 50, 75, 100 };
        }

        public async Task<IEnumerable<Result>> TopUp(int userId, IEnumerable<TopUpDTO> topUps)
        {
            var userResult = await _userRepository.FindBy(userId);
            if (!userResult.IsSuccess)
            {
                return new List<Result> { userResult };
            }

            var beneficiariesResult = await _beneficiaryRepository.GetAllBeneficiariesFor(userId);
            if (!beneficiariesResult.Any())
            {
                return new List<Result> { ResultFactory.Error("No beneficiaries found.") };
            }

            List<Shared.Entities.Beneficiary> beneficiariesData = beneficiariesResult!.ToList();

            User userDM = new()
            {
                Id = userResult.Data!.Id,
                IsVerified = userResult.Data.IsVerified,
                Beneficiaries = new Beneficiaries(
                    beneficiariesData.Select(b =>
                        new Beneficiary(b.Id,
                                        b.NickName,
                                        _topUpRepository.TopUpsForCurrentMonth(b.Id).Result.Select(t => t.Amount).ToList())).ToList()),
                Balance = userResult.Data!.Balance,
            };


            var topUpResults = userDM.TopUp(topUps.Select(t => new Recharge(t.Beneficiary.Id, t.Amount)).ToList()).ToList();

            var validTopUps = topUpResults.Where(x => x.IsSuccess).ToList();

            await _topUpRepository.TopUp(userResult.Data, validTopUps.Select(t => (new Shared.Entities.TopUp
            {
                Amount = t.Data!.Amount,
                BeneficiaryId = t.Data.BeneficiaryId,
                DateCreated = DateTime.UtcNow
            })));

            return topUpResults;
        }
    }
}

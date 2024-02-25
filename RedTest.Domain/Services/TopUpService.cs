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

        public TopUpService(ITopUpRepository topUpRepository, IUserRepository userRepository)
        {
            _topUpRepository = topUpRepository ?? throw new ArgumentNullException(nameof(topUpRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
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

            var beneficiariesResult = await _userRepository.GetBeneficiaries(userId);
            if (!beneficiariesResult.IsSuccess)
            {
                return new List<Result> { beneficiariesResult };
            }

            List<Shared.Entities.Beneficiary> beneficiariesData = beneficiariesResult.Data!.ToList();

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

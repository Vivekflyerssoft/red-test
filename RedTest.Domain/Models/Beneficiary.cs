using RedTest.Shared;

namespace RedTest.Domain.Models;

public class Beneficiary
{
    private readonly TopUpList _topUps;

    private const int VERIFIED_USER_BENEFICIARY_TOPUP_MAX_LIMIT = 500;
    private const int UNVERIFIED_USER_BENEFICIARY_TOPUP_MAX_LIMIT = 1000;

    public int Id { get; set; }

    public string NickName { get; set; }

    public Beneficiary(string nickName)
    {
        NickName = nickName;
        _topUps = new TopUpList();
    }

    public Beneficiary(int id, string nickName, List<uint> topUpsForCurrentMonth)
    {
        Id = id;
        NickName = nickName;
        _topUps = new TopUpList(topUpsForCurrentMonth);
    }

    public Result<Recharge> TopUp(uint withAmount, bool isVerifiedUser)
    {
        Result validationResult = EligibleForTopUp(withAmount, isVerifiedUser);
        if (!validationResult.IsSuccess)
        {
            return ResultFactory.Error<Recharge>(validationResult.ErrorMessage);
        }
        _topUps.Add(withAmount);
        return ResultFactory.Success(new Recharge(Id, withAmount));
    }

    private Result EligibleForTopUp(uint withAmount, bool isVerifiedUser)
    {
        uint sumOfTopUpAmountForCurrentMonth = GetSumOfTopUpAmountForCurrentMonth() + withAmount;
        if (isVerifiedUser && sumOfTopUpAmountForCurrentMonth > VERIFIED_USER_BENEFICIARY_TOPUP_MAX_LIMIT)
        {
            return ResultFactory.Error("TopUp failed, verified beneficiary top limit for the month is reached.");
        }
        else if (!isVerifiedUser && sumOfTopUpAmountForCurrentMonth > UNVERIFIED_USER_BENEFICIARY_TOPUP_MAX_LIMIT)
        {
            return ResultFactory.Error("TopUp failed, unverified beneficiary top limit for the month is reached.");
        }
        return ResultFactory.Success();
    }

    public uint GetSumOfTopUpAmountForCurrentMonth()
    {
        return _topUps.GetCurrentMonthTotalTopUpAmount();
    }
}

public record Recharge(int BeneficiaryId, uint Amount);
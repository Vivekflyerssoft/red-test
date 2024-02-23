namespace RedTest.Domain;

public class Beneficiary
{
    private const int VERIFIED_USER_BENEFICIARY_TOPUP_MAX_LIMIT = 500;
    private const int UNVERIFIED_USER_BENEFICIARY_TOPUP_MAX_LIMIT = 1000;
    private readonly TopUpList _topUps;

    public string NickName { get; set; }

    public Beneficiary(string nickName)
    {
        NickName = nickName;
        _topUps = new TopUpList();
    }

    public Result TopUp(uint withAmount, bool isVerifiedUser)
    {
        if (_topUps.IsRechargeDataAvailableForCurrentMonth)
        {
            uint sumOfTopUpAmountForCurrentMonth = _topUps.GetCurrentMonthTotalTopUpAmount() + withAmount;
            Result validationResult = Validate(sumOfTopUpAmountForCurrentMonth, isVerifiedUser);
            if (!validationResult.IsSuccess)
            {
                return validationResult;
            }
        }
        _topUps.Add(withAmount);
        return ResultFactory.Success();
    }

    private Result Validate(uint sumOfTopUpAmountForCurrentMonth, bool isVerifiedUser)
    {
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
}

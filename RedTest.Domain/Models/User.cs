using RedTest.Shared;

namespace RedTest.Domain.Models;

public class User
{
    private const int SUM_TOTAL_ALLOWED_RECHARGE_FOR_ALL_BENEFICIARIES = 3000;

    public Beneficiaries Beneficiaries { get; set; }

    public User()
    {
        Beneficiaries = new Beneficiaries();
    }

    public User(Beneficiaries beneficiaries)
    {
        Beneficiaries = beneficiaries;
    }

    public int Id { get; set; }

    public bool IsVerified { get; set; }

    public int Balance { get; set; }

    public Result AddBeneficiary(Beneficiary beneficiary)
    {
        return Beneficiaries.Add(beneficiary);
    }

    public IEnumerable<Beneficiary> GetAllBeneficiaries()
    {
        return Beneficiaries;
    }

    public IEnumerable<int> GetAvailableTopUpOptions()
    {
        return new List<int> { 5, 10, 20, 30, 50, 75, 100 };
    }

    public IEnumerable<Result<Recharge>> TopUp(List<Recharge> recharges)
    {
        if (Balance < recharges.Sum(x => x.Amount))
        {
            return new List<Result<Recharge>> { ResultFactory.Error<Recharge>("User has low balance.") };
        }
        long currentTotalTopUpValue = recharges.Sum(recharge => recharge.Amount);
        int sumTotalOfExistingTotalForCurrentMonth = Beneficiaries.GetSumOfTopUpAmountForCurrentMonth().Data;
        if (sumTotalOfExistingTotalForCurrentMonth + currentTotalTopUpValue > SUM_TOTAL_ALLOWED_RECHARGE_FOR_ALL_BENEFICIARIES)
        {
            return new List<Result<Recharge>> { ResultFactory.Error<Recharge>("Total recharge limit reached for all beneficiaries.") };
        }
        return recharges.Select(TopUpTransaction);
    }

    public Result<Recharge> TopUp(Recharge recharge)
    {
        IEnumerable<Result<Recharge>> result = TopUp(new List<Recharge> { recharge });
        return result.First();
    }

    private Result<Recharge> TopUpTransaction(Recharge recharge)
    {
        if (!GetAvailableTopUpOptions().Any(amount => amount == recharge.Amount))
        {
            return ResultFactory.Error<Recharge>("TopUp amount is not available. Please try with available TopUpOptions.");
        }
        Result<Beneficiary> containsBeneficiaryResult = Beneficiaries.Find(recharge.BeneficiaryId);

        if (!containsBeneficiaryResult.IsSuccess)
        {
            return ResultFactory.Error<Recharge>(containsBeneficiaryResult.ErrorMessage);
        }

        return containsBeneficiaryResult.Data!.TopUp(recharge.Amount, IsVerified);
    }
}
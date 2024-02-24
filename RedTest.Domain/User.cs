
namespace RedTest.Domain;

public class User
{
    private readonly Beneficiaries _beneficiaries;

    public User()
    {
        _beneficiaries = new Beneficiaries();
    }

    public bool IsVerified { get; set; }

    public Result AddBeneficiary(Beneficiary beneficiary)
    {
        return _beneficiaries.Add(beneficiary);
    }

    public IEnumerable<Beneficiary> GetAllBeneficiaries()
    {
        return _beneficiaries;
    }

    public IEnumerable<uint> GetAvailableTopUpOptions()
    {
        return new List<uint> { 5, 10, 20, 30, 50, 75, 100 };
    }

    public Result TopUp(Recharge recharge)
    {
        if (!GetAvailableTopUpOptions().Any(amount => amount == recharge.Amount))
        {
            return ResultFactory.Error<bool>("TopUp amount is not available. Please try with available TopUpOptions.");
        }
        Result containsBeneficiaryResult = _beneficiaries.Contains(recharge.Beneficiary);

        if (!containsBeneficiaryResult.IsSuccess)
        {
            return containsBeneficiaryResult;
        }

        return recharge.Beneficiary.TopUp(recharge.Amount, IsVerified);
    }

    public IEnumerable<Result> TopUp(List<Recharge> recharges)
    {
        return recharges.Select(recharge => TopUp(recharge));
    }
}
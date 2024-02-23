
using System.Linq;

namespace RedTest.Domain;

public class User
{
    private readonly Beneficiaries _beneficiaries;

    public User()
    {
        _beneficiaries = new Beneficiaries();
    }


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

    public Result TopUp(Beneficiary beneficiary, uint withAmount)
    {
        if (!GetAvailableTopUpOptions().Any(amount => amount == withAmount))
        {
            return ResultFactory.Error<bool>("TopUp amount is not available. Please try with available TopUpOptions.");
        }
        return _beneficiaries.Contains(beneficiary);
    }
}
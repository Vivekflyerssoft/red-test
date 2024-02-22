using System.Collections;
using System.Linq;

namespace RedTest.Domain;

public class User
{
    private readonly Beneficiaries _beneficiaries;

    public User()
    {
        _beneficiaries = new Beneficiaries();
    }


    public Result<bool> AddBeneficiary(Beneficiary beneficiary)
    {
        return _beneficiaries.Add(beneficiary);
    }

    public IEnumerable<Beneficiary> GetAllBeneficiaries()
    {
        return _beneficiaries;
    }
}
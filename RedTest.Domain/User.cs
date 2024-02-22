using System.Collections;
using System.Linq;

namespace RedTest.Domain;

public class User
{
    public User()
    {
        Beneficiaries = new Beneficiaries();
    }

    public Beneficiaries Beneficiaries { get; private set; }

    public bool AddBeneficiary(Beneficiary beneficiary)
    {
        return Beneficiaries.Add(beneficiary);
    }
}
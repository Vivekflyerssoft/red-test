using System.Collections;
namespace RedTest.Domain;

public class User
{
    public User()
    {
        Beneficiaries = new List<Beneficiary>();
    }
    public ICollection<Beneficiary> Beneficiaries { get; private set; }

    public void AddBeneficiary(Beneficiary beneficiary)
    {
        Beneficiaries.Add(beneficiary);
    }
}
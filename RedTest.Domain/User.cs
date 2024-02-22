using System.Collections;
namespace RedTest.Domain;

public class User
{
    public User()
    {
        Beneficiaries = new List<Beneficiary>();
    }
    public ICollection<Beneficiary> Beneficiaries { get; private set; }

    public bool AddBeneficiary(Beneficiary beneficiary)
    {
        if(Beneficiaries.Count == 5){
            return false;
        }
        Beneficiaries.Add(beneficiary);
        return true;
    }
}
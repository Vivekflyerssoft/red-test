using System.Collections;

namespace RedTest.Domain;

public class Beneficiaries : IEnumerable<Beneficiary>
{
    List<Beneficiary> beneficiaryList = new List<Beneficiary>();

    public Beneficiary this[int index]
    {
        get => beneficiaryList[index];
        set => beneficiaryList.Insert(index, value);
    }

    public IEnumerator<Beneficiary> GetEnumerator()
    {
        return beneficiaryList.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public bool Add(Beneficiary beneficiary)
    {
        if (beneficiaryList.Count == 5 || beneficiary.NickName.Length > 20)
        {
            return false;
        }
        beneficiaryList.Add(beneficiary);
        return true;
    }
}
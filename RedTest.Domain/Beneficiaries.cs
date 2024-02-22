using System.Collections;

namespace RedTest.Domain;

public class Beneficiaries : IEnumerable<Beneficiary>
{
    readonly List<Beneficiary> beneficiaryList = new List<Beneficiary>();

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

    public Result<bool> Add(Beneficiary beneficiary)
    {
        if (beneficiaryList.Count == 5)
        {
            return ResultFactory.Error<bool>("Cannot add beneficiary, reached beneficiaries max limit.");
        }
        if (beneficiary.NickName.Length > 20)
        {
            return ResultFactory.Error<bool>("Beneficiary nickname should have less than 20 characters.");
        }
        beneficiaryList.Add(beneficiary);
        return ResultFactory.Success(true);
    }
}

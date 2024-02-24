using System.Collections;

namespace RedTest.Domain;

public class Beneficiaries : IEnumerable<Beneficiary>
{
    readonly List<Beneficiary> beneficiaryList = new();

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

    public Result Add(Beneficiary beneficiary)
    {
        if (beneficiaryList.Count == 5)
        {
            return ResultFactory.Error("Cannot add beneficiary, reached beneficiaries max limit.");
        }
        if (beneficiary.NickName.Length > 20)
        {
            return ResultFactory.Error("Beneficiary nickname should have less than 20 characters.");
        }
        beneficiaryList.Add(beneficiary);
        return ResultFactory.Success();
    }

    public Result Contains(Beneficiary beneficiary)
    {
        return beneficiaryList.Any(x => x.NickName == beneficiary.NickName) ? ResultFactory.Success() : ResultFactory.Error("Please try to top up with a valid beneficiary.Please try to top up with a valid beneficiary.");
    }

    public Result<int> GetSumOfTopUpAmountForCurrentMonth()
    {
        return ResultFactory.Success((int)beneficiaryList.Sum(beneficiary => beneficiary.GetSumOfTopUpAmountForCurrentMonth()));
    }

}

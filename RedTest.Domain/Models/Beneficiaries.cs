using RedTest.Shared;
using System.Collections;

namespace RedTest.Domain.Models;

public class Beneficiaries : IEnumerable<Beneficiary>
{
    readonly List<Beneficiary> _beneficiaryList;

    public Beneficiaries()
    {
        _beneficiaryList = new();
    }

    public Beneficiaries(List<Beneficiary> list)
    {
        _beneficiaryList = list;
    }

    public Beneficiary this[int index]
    {
        get => _beneficiaryList[index];
        set => _beneficiaryList.Insert(index, value);
    }

    public IEnumerator<Beneficiary> GetEnumerator()
    {
        return _beneficiaryList.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public Result<Beneficiary> Add(Beneficiary beneficiary)
    {
        if (_beneficiaryList.Count == 5)
        {
            return ResultFactory.Error<Beneficiary>("Cannot add beneficiary, reached beneficiaries max limit.");
        }
        if (beneficiary.NickName.Length > 20)
        {
            return ResultFactory.Error<Beneficiary>("Beneficiary nickname should have less than 20 characters.");
        }
        _beneficiaryList.Add(beneficiary);
        return ResultFactory.Success(beneficiary);
    }

    public Result<Beneficiary> Find(int beneficiaryId)
    {
        var matchingBeneficiary = _beneficiaryList.FirstOrDefault(x => x.Id == beneficiaryId);
        return matchingBeneficiary != null ? ResultFactory.Success(matchingBeneficiary) : ResultFactory.Error<Beneficiary>("Please try to top up with a valid beneficiary.Please try to top up with a valid beneficiary.");
    }

    public Result<int> GetSumOfTopUpAmountForCurrentMonth()
    {
        return ResultFactory.Success((int)_beneficiaryList.Sum(beneficiary => beneficiary.GetSumOfTopUpAmountForCurrentMonth()));
    }

}

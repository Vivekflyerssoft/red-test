using System.Linq;

namespace RedTest.Domain;

public class Beneficiary
{
    private const int VERIFIED_USER_BENEFICIARY_TOPUP_MAX_LIMIT = 500;
    private const int UNVERIFIED_USER_BENEFICIARY_TOPUP_MAX_LIMIT = 1000;
    private readonly Dictionary<string, List<uint>> _topUps;

    public string NickName { get; set; }

    public Beneficiary(string nickName)
    {
        NickName = nickName;
        _topUps = new Dictionary<string, List<uint>>();
    }

    public Result TopUp(uint withAmount, bool isVerifiedUser)
    {
        var today = DateTime.Now;
        string key = $"{today.Month}/{today.Year}";
        if (_topUps.ContainsKey(key))
        {
            long amount = _topUps[key].Sum(x => x) + withAmount;
            if (isVerifiedUser && amount > VERIFIED_USER_BENEFICIARY_TOPUP_MAX_LIMIT)
            {
                return ResultFactory.Error("TopUp failed, verified beneficiary top limit for the month is reached.");
            }
            else if (!isVerifiedUser && amount > UNVERIFIED_USER_BENEFICIARY_TOPUP_MAX_LIMIT)
            {
                return ResultFactory.Error("TopUp failed, unverified beneficiary top limit for the month is reached.");
            }
            _topUps[key].Add(withAmount);
        }
        else
        {
            _topUps.Add(key, new List<uint> { withAmount });
        }
        return ResultFactory.Success();
    }

}

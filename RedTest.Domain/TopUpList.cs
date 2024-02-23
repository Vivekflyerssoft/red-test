namespace RedTest.Domain;

public class TopUpList
{
    private readonly Dictionary<string, List<uint>> _topUps;
    private readonly string key;

    public TopUpList()
    {
        _topUps = new Dictionary<string, List<uint>>();
        DateTime today = DateTime.Now;
        key = $"{today.Month}/{today.Year}";
    }

    public bool IsRechargeDataAvailableForCurrentMonth => _topUps.ContainsKey(key);

    public uint GetCurrentMonthTotalTopUpAmount()
    {
        uint sumOfTopUpAmountForCurrentMonth = 0;

        if (_topUps.ContainsKey(key))
        {
            sumOfTopUpAmountForCurrentMonth = (uint)_topUps[key].Sum(x => x);
        }

        return sumOfTopUpAmountForCurrentMonth;
    }

    public void Add(uint withAmount)
    {
        if (_topUps.ContainsKey(key))
        {
            _topUps[key].Add(withAmount);
        }
        else
        {
            _topUps.Add(key, new List<uint> { withAmount });
        }
    }

}

namespace RedTest.Domain.Models;

public class TopUpList
{
    private readonly IDictionary<string, List<uint>> _topUps;
    private readonly string key = $"{DateTime.Now.Month}/{DateTime.Now.Year}";

    public TopUpList()
    {
        _topUps = new Dictionary<string, List<uint>>();
    }

    public TopUpList(List<uint> topUpsForCurrentMonth)
    {
        _topUps = new Dictionary<string, List<uint>>
        {
            {key, topUpsForCurrentMonth }
        };
    }

    public bool IsRechargeDataAvailableForCurrentMonth => _topUps.ContainsKey(key);

    public uint GetCurrentMonthTotalTopUpAmount()
    {
        uint sumOfTopUpAmountForCurrentMonth = 0;

        if (_topUps.TryGetValue(key, out var values))
        {
            sumOfTopUpAmountForCurrentMonth = (uint)values.Sum(x => x);
        }

        return sumOfTopUpAmountForCurrentMonth;
    }

    public void Add(uint withAmount)
    {
        if (_topUps.TryGetValue(key, out var value))
        {
            value.Add(withAmount);
        }
        else
        {
            _topUps.Add(key, new List<uint> { withAmount });
        }
    }

}

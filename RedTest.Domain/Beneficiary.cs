namespace RedTest.Domain;

public class Beneficiary
{
    public string NickName { get; set; }

    public Beneficiary(string nickName)
    {
        NickName = nickName;
    }
}

namespace RedTest.Domain.Test;

static class UserTestsFactory
{
    private const string fake_nickname = "fake_nickname";
    private const string fake_nickname_with_more_than_20_characters = "fake_nickname_more_than_twenty_characters";

    public static User CreateFakeUser()
    {
        return new User();
    }

    public static User AddFakeBeneficiary(this User user, string nickName = fake_nickname)
    {
        Beneficiary beneficiary = CreateFakeBeneficiary(nickName);
        user.AddBeneficiary(beneficiary);
        return user;
    }

    public static Beneficiary CreateFakeBeneficiary(string nickName = fake_nickname)
    {
        return new Beneficiary(nickName);
    }
}
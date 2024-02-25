using RedTest.Domain.Models;

namespace RedTest.Domain.Test;

static class UserTestsFactory
{
    private const string fake_nickname = "fake_nickname";
    private const string fake_nickname_with_more_than_20_characters = "fake_nickname_more_than_twenty_characters";

    public static User CreateFakeUser()
    {
        return new User();
    }

    public static User Verified(this User user)
    {
        user.IsVerified = true;
        return user;
    }

    public static User WithInitialBalance(this User user, int balance)
    {
        user.Balance = balance;
        return user;
    }

    public static User AddFakeBeneficiary(this User user, string nickName = fake_nickname)
    {
        Beneficiary beneficiary = CreateFakeBeneficiary(nickName, user.Beneficiaries.Count());
        user.AddBeneficiary(beneficiary);
        return user;
    }

    public static User AddFakeBeneficiaries(this User user, int count)
    {
        Enumerable.Range(1, count).ToList().ForEach(index =>
        {

            Beneficiary beneficiary = CreateFakeBeneficiary($"{fake_nickname}_{index}", index + 1);
            user.AddBeneficiary(beneficiary);
        });
        return user;
    }

    public static Beneficiary CreateFakeBeneficiary(string nickName = fake_nickname, int id = 1)
    {
        return new Beneficiary(nickName) { Id = id };
    }

    public static void TopUp(this User user, Beneficiary beneficiary, uint withAmount, int times)
    {
        Enumerable.Range(0, times).ToList().ForEach((index) =>
        {
            user.TopUp(new Recharge(beneficiary.Id, withAmount));
        });
    }
}
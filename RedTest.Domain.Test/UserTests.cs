namespace RedTest.Domain.Test;

public class UserTests
{
    private const string fake_nickname = "fake_nickname";
    private const string fake_nickname_with_more_than_20_characters = "fake_nickname_more_than_twenty_characters";

    [Fact]
    public void User_Should_Be_Able_To_Add_Beneficiaries()
    {
        User user = new User();
        Beneficiary beneficiary = CreateFakeBeneficiary();

        user.AddBeneficiary(beneficiary);

        user.Beneficiaries.Should().NotBeNull();
    }

    [Fact]
    public void User_Should_Not_Be_Allowed_To_Add_More_Than_5_Beneficiary()
    {
        User user = new User();

        user.AddBeneficiary(CreateFakeBeneficiary());
        user.AddBeneficiary(CreateFakeBeneficiary());
        user.AddBeneficiary(CreateFakeBeneficiary());
        user.AddBeneficiary(CreateFakeBeneficiary());
        user.AddBeneficiary(CreateFakeBeneficiary());

        user.AddBeneficiary(CreateFakeBeneficiary()).Should().BeFalse();
        user.Beneficiaries.Should().HaveCount(5);
    }

    [Fact]
    public void User_Beneficiary_Should_Have_Allowed_NickName()
    {
        User user = new User();
        Beneficiary beneficiary = CreateFakeBeneficiary();

        user.AddBeneficiary(beneficiary);

        user.Beneficiaries.First().Should().BeEquivalentTo(new { NickName = fake_nickname });
    }

    [Fact]
    public void User_Beneficiary_Add_Should_Not_Be_Created_For_NickName_More_Than_20_Characters()
    {
        User user = new User();
        Beneficiary beneficiary = CreateFakeBeneficiary(fake_nickname_with_more_than_20_characters);

        bool result = user.AddBeneficiary(beneficiary);

        result.Should().BeFalse();
    }

    Beneficiary CreateFakeBeneficiary()
    {
        return CreateFakeBeneficiary(fake_nickname);
    }

    Beneficiary CreateFakeBeneficiary(string nickName)
    {
        return new Beneficiary(nickName);
    }

}
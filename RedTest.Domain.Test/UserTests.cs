namespace RedTest.Domain.Test;

public class UserTests
{
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
    public void User_Beneficiary_Should_Be_Allowed_NickName()
    {
        User user = new User();
        Beneficiary beneficiary = CreateFakeBeneficiary();

        user.AddBeneficiary(beneficiary);

        user.Beneficiaries.First().Should().BeEquivalentTo(new {NickName = "fake_nickname"});
    }

    Beneficiary CreateFakeBeneficiary()
    {
        return new Beneficiary("fake_nickname");
    }

}
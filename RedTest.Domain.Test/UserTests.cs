namespace RedTest.Domain.Test;

public class UserTests
{
    [Fact]
    public void User_Should_Be_Able_To_Add_Beneficiaries()
    {
        User user = new User();
        Beneficiary beneficiary = new Beneficiary();

        user.AddBeneficiary(beneficiary);

        user.Beneficiaries.Should().NotBeNull();
    }

    [Fact]
    public void User_Should_Not_Be_Allowed_To_Add_More_Than_5_Beneficiary()
    {
        User user = new User();
        Beneficiary beneficiaryOne = new Beneficiary();
        Beneficiary beneficiaryTwo = new Beneficiary();
        Beneficiary beneficiaryThree = new Beneficiary();
        Beneficiary beneficiaryFour = new Beneficiary();
        Beneficiary beneficiaryFive = new Beneficiary();
        Beneficiary beneficiarySix = new Beneficiary();

        user.AddBeneficiary(beneficiaryOne);
        user.AddBeneficiary(beneficiaryTwo);
        user.AddBeneficiary(beneficiaryThree);
        user.AddBeneficiary(beneficiaryFour);
        user.AddBeneficiary(beneficiaryFive);

        user.AddBeneficiary(beneficiarySix).Should().BeFalse();
        user.Beneficiaries.Should().HaveCount(5);
    }
}
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
}
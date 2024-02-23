namespace RedTest.Domain.Test;

public class UserTests
{

    private readonly User user;

    public UserTests()
    {
        user = UserTestsFactory.CreateFakeUser();
    }

    [Fact]
    public void User_Should_Be_Able_To_Add_Beneficiaries()
    {
        user.AddFakeBeneficiary();

        user.GetAllBeneficiaries().Should().NotBeNull();
    }

    [Fact]
    public void User_Should_Not_Be_Allowed_To_Add_More_Than_5_Beneficiary()
    {
        user.AddFakeBeneficiary();
        user.AddFakeBeneficiary();
        user.AddFakeBeneficiary();
        user.AddFakeBeneficiary();
        user.AddFakeBeneficiary();

        Result result = user.AddBeneficiary(UserTestsFactory.CreateFakeBeneficiary());
        result.IsSuccess.Should().BeFalse();
        result.ErrorMessage.Should().Be("Cannot add beneficiary, reached beneficiaries max limit.");
        user.GetAllBeneficiaries().Should().HaveCount(5);
    }

    [Fact]
    public void User_Beneficiary_Should_Have_NickName()
    {
        user.AddFakeBeneficiary();

        user.GetAllBeneficiaries().First().Should().BeEquivalentTo(new { NickName = "fake_nickname" });
    }

    [Fact]
    public void User_Beneficiary_Add_Should_Not_Be_Created_For_NickName_More_Than_20_Characters()
    {
        Beneficiary beneficiary = UserTestsFactory.CreateFakeBeneficiary("fake_nickname_more_than_twenty_characters");

        var result = user.AddBeneficiary(beneficiary);

        result.IsSuccess.Should().BeFalse();
        result.ErrorMessage.Should().Be("Beneficiary nickname should have less than 20 characters.");
    }

    [Fact]
    public void User_Should_Be_Able_To_View_All_Beneficiaries()
    {
        user.AddFakeBeneficiary();
        user.AddFakeBeneficiary();

        user.GetAllBeneficiaries().Should().HaveCount(2);
    }

    [Fact]
    public void User_Should_Be_Able_To_View_All_Topup_Options()
    {
        IEnumerable<uint> result = user.GetAvailableTopUpOptions();

        result.Should().BeEquivalentTo(new List<int> { 5, 10, 20, 30, 50, 75, 100 });
    }

    [Fact]
    public void User_Should_Be_Allowed_To_TopUp_Beneficiary()
    {
        user.AddFakeBeneficiary();

        Beneficiary beneficiary = UserTestsFactory.CreateFakeBeneficiary();
        uint withAmount = 10;

        Result result = user.TopUp(beneficiary, withAmount);

        result.IsSuccess.Should().BeTrue();

    }

    [Fact]
    public void User_Should_Not_Be_Allowed_To_TopUp_Beneficiary_If_TopUp_Amount_Not_Available()
    {
        user.AddFakeBeneficiary();

        Beneficiary beneficiary = UserTestsFactory.CreateFakeBeneficiary();
        uint withAmount = 15;

        Result result = user.TopUp(beneficiary, withAmount);

        result.IsSuccess.Should().BeFalse();
        result.ErrorMessage.Should().Be("TopUp amount is not available. Please try with available TopUpOptions.");
    }

    [Fact]
    public void User_Should_Not_Be_Allowed_To_TopUp_An_Unknown_Beneficiary()
    {
        user.AddFakeBeneficiary();

        Beneficiary beneficiary = UserTestsFactory.CreateFakeBeneficiary("fake_nickname_2");
        uint withAmount = 10;

        Result result = user.TopUp(beneficiary, withAmount);

        result.IsSuccess.Should().BeFalse();
        result.ErrorMessage.Should().Be("Please try to top up with a valid beneficiary.Please try to top up with a valid beneficiary.");
}
}

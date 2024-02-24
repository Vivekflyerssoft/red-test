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

        Result result = user.TopUp(new Recharge(beneficiary, withAmount));

        result.IsSuccess.Should().BeTrue();

    }

    [Fact]
    public void User_Should_Not_Be_Allowed_To_TopUp_Beneficiary_If_TopUp_Amount_Not_Available()
    {
        user.AddFakeBeneficiary();

        Beneficiary beneficiary = UserTestsFactory.CreateFakeBeneficiary();
        uint withAmount = 15;

        Result result = user.TopUp(new Recharge(beneficiary, withAmount));

        result.IsSuccess.Should().BeFalse();
        result.ErrorMessage.Should().Be("TopUp amount is not available. Please try with available TopUpOptions.");
    }

    [Fact]
    public void User_Should_Not_Be_Allowed_To_TopUp_An_Unknown_Beneficiary()
    {
        user.AddFakeBeneficiary();

        Beneficiary beneficiary = UserTestsFactory.CreateFakeBeneficiary("fake_nickname_2");
        uint withAmount = 10;

        Result result = user.TopUp(new Recharge(beneficiary, withAmount));

        result.IsSuccess.Should().BeFalse();
        result.ErrorMessage.Should().Be("Please try to top up with a valid beneficiary.Please try to top up with a valid beneficiary.");
    }

    [Fact]
    public void VerifiedUser_Should_Be_Allowed_To_TopUp_UpTo_500_Per_Month_Per_Beneficiary()
    {
        user.Verified().AddFakeBeneficiary();
        Beneficiary beneficiary = user.GetAllBeneficiaries().First();
        user.TopUp(beneficiary, 100, 4);

        Result result = user.TopUp(new Recharge(beneficiary, 100));

        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public void VerifiedUser_Should_Not_Be_Allowed_To_TopUp_More_500_Per_Month_Per_Beneficiary()
    {
        user.Verified().AddFakeBeneficiary();
        Beneficiary beneficiary = user.GetAllBeneficiaries().First();
        user.TopUp(beneficiary, 100, 5);

        Result result = user.TopUp(new Recharge(beneficiary, 5));

        result.IsSuccess.Should().BeFalse();
        result.ErrorMessage.Should().Be("TopUp failed, verified beneficiary top limit for the month is reached.");
    }

    [Fact]
    public void UnVerifiedUser_Should_Be_Allowed_To_TopUp_UpTo_1000_Per_Month_Per_Beneficiary()
    {
        user.AddFakeBeneficiary();
        Beneficiary beneficiary = user.GetAllBeneficiaries().First();
        user.TopUp(beneficiary, 100, 9);

        Result result = user.TopUp(new Recharge(beneficiary, 100));

        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public void UnVerifiedUser_Should_Not_Be_Allowed_To_TopUp_More_1000_Per_Month_Per_Beneficiary()
    {
        user.AddFakeBeneficiary();
        Beneficiary beneficiary = user.GetAllBeneficiaries().First();
        user.TopUp(beneficiary, 100, 10);

        Result result = user.TopUp(new Recharge(beneficiary, 5));

        result.IsSuccess.Should().BeFalse();
        result.ErrorMessage.Should().Be("TopUp failed, unverified beneficiary top limit for the month is reached.");
    }

    [Fact]
    public void User_Should_Be_Allowed_To_TopUp_Multiple_Beneficiaries()
    {
        user.AddFakeBeneficiary("fake_beneficiary_one").AddFakeBeneficiary("fake_beneficiary_two");

        IEnumerable<Beneficiary> beneficiaries = user.GetAllBeneficiaries();
        List<Recharge> recharges = new()
        {
            new(beneficiaries.First(), 10),
            new(beneficiaries.Skip(1).First(), 10)
        };
        IEnumerable<Result> result = user.TopUp(recharges);

        result.Should().AllBeEquivalentTo(new {IsSuccess = true});
    }
}



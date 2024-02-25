using NSubstitute;
using RedTest.Domain.Services;
using RedTest.Shared.Entities;
using RedTest.Shared.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedTest.Domain.Test
{
    public class TopUpServiceTests
    {
        private readonly TopUpService _sut;
        private readonly ITopUpRepository _topUpRepository;
        private readonly IUserRepository _userRepository;

        public TopUpServiceTests()
        {
            _topUpRepository = Substitute.For<ITopUpRepository>();
            _userRepository = Substitute.For<IUserRepository>();
            _sut = new TopUpService(_topUpRepository, _userRepository);
        }

        [Fact]
        public void User_Should_Be_Able_To_View_All_Topup_Options()
        {
            IEnumerable<uint> result = _sut.GetAvailableTopUpOptions();

            result.Should().BeEquivalentTo(new List<int> { 5, 10, 20, 30, 50, 75, 100 });
        }
    }
}

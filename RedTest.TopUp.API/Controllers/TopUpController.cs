using Microsoft.AspNetCore.Mvc;
using RedTest.Domain;
using RedTest.Shared;
using RedTest.Shared.DTOs;
using RedTest.Shared.Entities;
using RedTest.Shared.Services;
using System.Net;

namespace RedTest.TopUp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopUpController : ApiBaseController<TopUpController>
    {
        private readonly ITopUpService _topUpService;

        public TopUpController(ITopUpService topUpService, ILogger<TopUpController> logger): base(logger)
        {
            _topUpService = topUpService ?? throw new ArgumentNullException(nameof(topUpService));
        }

        [HttpGet("AvailableOptions")]
        [ProducesResponseType(typeof(List<uint>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetTopUpOptions()
        {
            return Ok(_topUpService.GetAvailableTopUpOptions());
        }

        [HttpPost("User/{userId}")]
        [ProducesResponseType(typeof(List<Result>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Post(int userId, IEnumerable<TopUpDTO> topUps)
        {
            return Ok(await _topUpService.TopUp(userId, topUps));
        }
    }
}

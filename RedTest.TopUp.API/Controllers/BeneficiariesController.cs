using Microsoft.AspNetCore.Mvc;
using RedTest.Shared.DTOs;
using RedTest.Shared.Services;
using System.Net;

namespace RedTest.TopUp.API.Controllers
{
    [Route("api/{userId}/[controller]")]
    [ApiController]
    public class BeneficiariesController : ApiBaseController<BeneficiariesController>
    {
        private readonly IBeneficiaryService _beneficiaryService;

        public BeneficiariesController(IBeneficiaryService beneficiaryService, ILogger<BeneficiariesController> logger) : base(logger)
        {
            _beneficiaryService = beneficiaryService ?? throw new ArgumentNullException(nameof(beneficiaryService));
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IEnumerable<BeneficiaryDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(int userId)
        {
            return Ok(await _beneficiaryService.GetBeneficiaries(userId));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BeneficiaryDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> Post(int userId, BeneficiaryDTO beneficiary)
        {
            var result = await _beneficiaryService.Create(userId, beneficiary);
            if (!result.IsSuccess)
            {
                return BadRequest(result.ErrorMessage);
            }
            return Ok(result.Data);
        }
    }
}

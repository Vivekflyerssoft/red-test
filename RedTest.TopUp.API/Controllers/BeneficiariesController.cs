using Microsoft.AspNetCore.Mvc;
using RedTest.Shared.DTOs;
using System.Net;

namespace RedTest.TopUp.API.Controllers
{
    [Route("api/{userId}/[controller]")]
    [ApiController]
    public class BeneficiariesController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IEnumerable<BeneficiaryDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(int userId)
        {
            return Ok();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<IActionResult> Post(BeneficiaryDTO beneficiary)
        {
            return Ok();
        }
    }
}

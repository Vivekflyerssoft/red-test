using Microsoft.AspNetCore.Mvc;
using RedTest.Domain;
using RedTest.Shared;
using RedTest.Shared.DTOs;
using RedTest.Shared.Entities;
using System.Net;

namespace RedTest.TopUp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopUpController : ControllerBase
    {
        [HttpGet("/AvailableOptions")]
        [ProducesResponseType(typeof(List<uint>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetTopUpOptions()
        {
            return Ok(new List<uint> { 5, 10, 20, 30, 50, 75, 100 });
        }

        [HttpPost]
        [ProducesResponseType(typeof(List<Result>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Post(TopUpDTO topUp)
        {
            return null;
        }
    }
}

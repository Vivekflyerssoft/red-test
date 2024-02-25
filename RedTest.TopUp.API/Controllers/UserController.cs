using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RedTest.Shared.DTOs;
using RedTest.Shared;
using RedTest.Shared.Services;
using System.Net;

namespace RedTest.TopUp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ApiBaseController<UserController>
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService, ILogger<UserController> logger) : base(logger)
        {
            _userService = userService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(UserDTO), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Post(UserDTO user)
        {
            var result = await _userService.Create(user);
            if (!result.IsSuccess)
            {
                return BadRequest(result.ErrorMessage);
            }
            return Ok(result.Data);
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace RedTest.TopUp.API.Controllers
{
    public abstract class ApiBaseController<T> : ControllerBase
    {
        protected readonly ILogger _logger;

        protected ApiBaseController(ILogger<T> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
    }
}

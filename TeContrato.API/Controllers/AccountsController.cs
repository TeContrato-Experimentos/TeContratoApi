using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TeContrato.API.Domain.Services;
using TeContrato.API.Domain.Services.Communications;
using TeContrato.API.Extensions;

namespace TeContrato.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/api/[controller]")]
    [Produces("application/json")]
    public class AccountsController : ControllerBase
    {
        private IAccountService _accountService;

        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        [SwaggerOperation(Summary = "Verify credentials")]
        public IActionResult Authenticate([FromBody] AuthenticationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            var response = _accountService.Authenticate(request);

            if (response.Message != null)
                return BadRequest(new {message = response.Message});

            return Ok(response);
        }
    }
}
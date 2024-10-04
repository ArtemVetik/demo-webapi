using Entities.Dto;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace DemoWebApi.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private AccountService _service;

        public AccountController(AccountService service)
        {
            _service = service;
        }

        /// <summary>
        /// Sends a confirmation code to the email.
        /// </summary>
        /// <param name="email">Target email address</param>
        /// <returns></returns>
        /// <response code="200">Successful sending</response>
        [HttpPost("confirm-email")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> SendConfirmationCode([FromBody] ConfirmEmailDto email)
        {
            await _service.SendConfirmCode(email.email);
            return Ok();
        }

        /// <summary>
        /// Creates a new user profile.
        /// </summary>
        /// <param name="registrationData"></param>
        /// <returns></returns>
        /// <response code="200">Successful registration</response>
        /// <response code="400">Invalid condirmation code or attempting to create an existing account</response>
        [HttpPost("registration")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Registration([FromBody] RegistrationDto registrationData)
        {
            if (await _service.ConsumeConfirmationCode(registrationData.email, registrationData.confirm_code) == false)
                return BadRequest("Invalid confirmation code");

            if (await _service.Registration(registrationData) == false)
                return BadRequest("Account already exist");

            return Ok();
        }

        /// <summary>
        /// Performs a login and updates the refresh token.
        /// </summary>
        /// <param name="loginData">Login data</param>
        /// <returns>Access and refresh tokens</returns>
        /// <response code="200">Returns access and refresh tokens</response>
        /// <response code="400">If the login data is incorrect</response>
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public async Task<ActionResult<LoginTokensDto>> Login([FromBody] LoginCredentialsDto loginData)
        {
            var loginTokens = await _service.Login(loginData);

            if (loginTokens == null)
                return BadRequest("Invalid credentials");

            return Ok(loginTokens);
        }

        /// <summary>
        /// Create new access token by refresh token.
        /// </summary>
        /// <param name="refreshToken">Refresh token</param>
        /// <returns>Access token</returns>
        /// <response code="200">Returns access token</response>
        /// <response code="400">If the refresh token is invalid</response>
        [HttpPost("refresh")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public async Task<ActionResult<LoginTokensDto>> Login([FromBody] string refreshToken)
        {
            var loginTokens = await _service.Refresh(refreshToken);

            if (loginTokens == null)
                return BadRequest("Invalid token");

            return Ok(loginTokens);
        }
    }
}

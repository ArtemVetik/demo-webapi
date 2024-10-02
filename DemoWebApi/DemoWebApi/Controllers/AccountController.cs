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

        [HttpPost("confirm-email")]
        public async Task<IActionResult> SendConfirmationCode([FromBody] ConfirmEmailDto email)
        {
            await _service.SendConfirmCode(email.email);
            return Ok();
        }

        [HttpPost("registration")]
        public async Task<IActionResult> Registration([FromBody] RegistrationDto registrationData)
        {
            if (await _service.ConsumeConfirmationCode(registrationData.email, registrationData.confirm_code) == false)
                return BadRequest("Invalid confirmation code");

            if (await _service.Registration(registrationData) == false)
                return BadRequest("Account already exist");

            return Ok();
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginTokensDto>> Login([FromBody] LoginCredentialsDto loginData)
        {
            var loginTokens = await _service.Login(loginData);

            if (loginTokens == null)
                return BadRequest("Invalid credentials");

            return Ok(loginTokens);
        }
    }
}

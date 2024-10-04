using DemoWebApi.Extentions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace DemoWebApi.Controllers
{
    [ApiController]
    [Route("api/profile")]
    public class ProfileController : ControllerBase
    {
        private ProfileService _service;

        public ProfileController(ProfileService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetProfile()
        {
            var playerId = User.GetUserId();

            if (string.IsNullOrEmpty(playerId))
                return Unauthorized();

            var profileDto = await _service.GetProfile(playerId);

            if (profileDto == null)
                return BadRequest("User not found");

            return Ok(profileDto);
        }
    }
}

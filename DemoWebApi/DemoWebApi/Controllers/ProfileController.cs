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
            var playerId = User.FindFirst("id");

            if (playerId == null)
                return BadRequest("User not found");

            var profileDto = await _service.GetProfile(playerId.Value);

            if (profileDto == null)
                return BadRequest("User not found");

            return Ok(profileDto);
        }
    }
}

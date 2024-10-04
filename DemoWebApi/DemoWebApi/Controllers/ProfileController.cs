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

        /// <summary>
        /// Returns profile data.
        /// </summary>
        /// <returns>Profile data</returns>
        /// <response code="200">Returns profile data</response>
        /// <response code="400">If user profile not found</response>
        /// <response code="401">If authorization error</response>
        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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

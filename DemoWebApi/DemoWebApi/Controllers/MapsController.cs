using DemoWebApi.Extentions;
using DemoWebApi.Filters;
using Entities.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace DemoWebApi.Controllers
{
    [ApiController]
    [Route("api/maps")]
    public class MapsController : ControllerBase
    {
        private MapService _service;

        public MapsController(MapService service)
        {
            _service = service;
        }

        /// <summary>
        /// Return array of all maps.
        /// </summary>
        /// <returns>Array of all maps</returns>
        /// <response code="200">Successful</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Produces("application/json")]
        public async Task<IActionResult> GetAll()
        {
            var data = await _service.GetAllMaps();
            return Ok(data);
        }

        /// <summary>
        /// Provides access to map download.
        /// </summary>
        /// <param name="mapId">Map identifier</param>
        /// <returns>Download url</returns>
        /// <response code="200">Returns download url</response>
        /// <response code="400">If map not found</response>
        /// <response code="401">If authorization error</response>
        [Authorize]
        [HttpPost("download/{mapId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Download(string mapId)
        {
            var playerId = User.GetUserId();

            if (string.IsNullOrEmpty(playerId))
                return Unauthorized();

            var downloadUrl = await _service.DownloadMap(playerId, mapId);

            if (downloadUrl == null)
                return BadRequest("Map not found");

            return Ok(downloadUrl);
        }

        /// <summary>
        /// Uploading new map.
        /// </summary>
        /// <param name="mapData">Map data</param>
        /// <returns></returns>
        /// <response code="200">Successful upload</response>
        /// <response code="401">If authorization error</response>
        [Authorize]
        [ServiceFilter(typeof(SubscriptionFilter))]
        [HttpPost("upload")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Upload([FromBody] UploadMapDto mapData)
        {
            var playerId = User.GetUserId();

            if (string.IsNullOrEmpty(playerId))
                return Unauthorized();

            await _service.UploadMap(playerId, mapData);
            return Ok();
        }
    }
}

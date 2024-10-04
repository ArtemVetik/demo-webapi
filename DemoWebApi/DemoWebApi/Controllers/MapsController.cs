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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _service.GetAllMaps();
            return Ok(data);
        }

        [Authorize]
        [HttpPost("download/{mapId}")]
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

        [Authorize]
        [ServiceFilter(typeof(SubscriptionFilter))]
        [HttpPost("upload")]
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

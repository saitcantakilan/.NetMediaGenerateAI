using Microsoft.AspNetCore.Mvc;
using PexelsDotNetSDK.Api;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MediaGeneratorController : ControllerBase
    {

        private readonly IConfiguration _configuration;

        public MediaGeneratorController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [Route("Videos")]
        [HttpGet]
        public async Task<IActionResult> GenerateVideos(string search)
        {
            var client = new PexelsClient(_configuration["PexelsApiToken"]);
            var searchVideos = client.SearchVideosAsync(search);
            List<string> result = searchVideos.Result.videos.Select(s => s.videoFiles.FirstOrDefault().link).ToList();
            return Ok(result);
        }

        [Route("Photos")]
        [HttpGet]
        public async Task<IActionResult> GeneratePhotos(string search)
        {
            var client = new PexelsClient(_configuration["PexelsApiToken"]);
            var searchVideos = client.SearchPhotosAsync(search);
            List<string> result = searchVideos.Result.photos.Select(s => s.source.original).ToList();
            return Ok(result);
        }
    }
}

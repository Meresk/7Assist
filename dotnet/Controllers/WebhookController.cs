using LiveKit.API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace dotnet8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebhookController : ControllerBase
    {
        private readonly ILiveKitService _liveKitService;

        public WebhookController(ILiveKitService liveKitService)
        {
            _liveKitService = liveKitService;
        }

        [HttpPost("livekit/webhook")]
        public IActionResult HandleWebhook([FromBody] string body, [FromHeader(Name = "Authorization")] string authHeader)
        {
            try
            {
                _liveKitService.VerifyWebhookEvent(authHeader, body);
                return Ok();
            }
            catch (Exception ex)
            {
                return Unauthorized(new { errorMessage = ex.Message });
            }
        }
    }
}

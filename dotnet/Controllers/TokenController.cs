using LiveKit.API.Interfaces;
using LiveKit.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace dotnet8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly ILiveKitService _liveKitService;

        public TokenController(ILiveKitService liveKitService)
        {
            _liveKitService = liveKitService;
        }

        [HttpPost]
        public IActionResult GenerateToken([FromBody] TokenRequest request)
        {
            if (string.IsNullOrEmpty(request.RoomName) || string.IsNullOrEmpty(request.ParticipantName))
            {
                return BadRequest(new { errorMessage = "roomName and participantName are required" });
            }

            var token = _liveKitService.CreateLiveKitJWT(request.RoomName, request.ParticipantName);
            return Ok(new { token });
        }
    }
}

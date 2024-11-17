using LiveKit.API.Interfaces;
using LiveKit.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace dotnet8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly ILiveKitService _liveKitService;
        private readonly string _jwtSecretKey;

        public TokenController(ILiveKitService liveKitService, IConfiguration config)
        {
            _liveKitService = liveKitService;
            _jwtSecretKey = config.GetValue<string>("JWT:Key");
        }

        [HttpPost]
        public IActionResult GenerateToken([FromBody] TokenRequest request)
        {
            // Извлекаем токен из cookies
            if (!Request.Cookies.TryGetValue("A", out string authToken) || string.IsNullOrEmpty(authToken))
            {
                return Unauthorized(new { errorMessage = "Authorization token is missing in cookies" });
            }

            try
            {
                // Проверяем, что токен JWT действителен
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadToken(authToken) as JwtSecurityToken;

                if (jwtToken == null || !ValidateJwtToken(authToken))
                {
                    return Unauthorized(new { errorMessage = "Invalid or expired token" });
                }
            }
            catch (Exception)
            {
                return Unauthorized(new { errorMessage = "Invalid token format" });
            }


            if (string.IsNullOrEmpty(request.RoomName) || string.IsNullOrEmpty(request.ParticipantName))
            {
                return BadRequest(new { errorMessage = "roomName and participantName are required" });
            }

            var token = _liveKitService.CreateLiveKitJWT(request.RoomName, request.ParticipantName);
            return Ok(new { token });
        }

        private bool ValidateJwtToken(string authToken)
        {
            var handler = new JwtSecurityTokenHandler();
            try
            {
                // Настройки для валидации токена
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,  // Не проверяем Issuer в этом примере
                    ValidateAudience = false, // Не проверяем Audience
                    ValidateLifetime = true, // Проверка срока действия токена
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSecretKey)) // Используем секретный ключ для проверки подписи
                };

                // Валидация токена
                var principal = handler.ValidateToken(authToken, tokenValidationParameters, out var validatedToken);
                return validatedToken is JwtSecurityToken && validatedToken.ValidTo > DateTime.UtcNow;
            }
            catch
            {
                return false; // Если токен невалиден или произошла ошибка при валидации
            }
        }
    }
}

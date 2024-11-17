using LiveKit.API.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace LiveKit.API.Services
{
    public class LiveKitService : ILiveKitService
    {
        private readonly string _liveKitApiKey;
        private readonly string _liveKitApiSecret;

        // Конструктор получает настройки из конфигурации
        public LiveKitService(IConfiguration config)
        {
            _liveKitApiKey = config.GetValue<string>("LIVEKIT_API_KEY");
            _liveKitApiSecret = config.GetValue<string>("LIVEKIT_API_SECRET");
        }

        public string CreateLiveKitJWT(string roomName, string participantName)
        {
            // Логика создания JWT для подключения к комнате LiveKit
            var videoGrants = new Dictionary<string, object>
        {
            { "room", roomName },
            { "roomJoin", true }
        };

            JwtHeader headers = new(new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_liveKitApiSecret)), "HS256"));
            JwtPayload payload = new()
        {
            { "exp", new DateTimeOffset(DateTime.UtcNow.AddHours(6)).ToUnixTimeSeconds() },
            { "iss", _liveKitApiKey },
            { "nbf", 0 },
            { "sub", participantName },
            { "name", participantName },
            { "video", videoGrants }
        };

            JwtSecurityToken token = new(headers, payload);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public void VerifyWebhookEvent(string authHeader, string body)
        {
            // Логика проверки подписи вебхука
            var utf8Encoding = new UTF8Encoding();
            var tokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(utf8Encoding.GetBytes(_liveKitApiSecret)),
                ValidateIssuer = true,
                ValidIssuer = _liveKitApiKey,
                ValidateAudience = false
            };

            var jwtValidator = new JwtSecurityTokenHandler();
            var claimsPrincipal = jwtValidator.ValidateToken(authHeader, tokenValidationParameters, out SecurityToken validatedToken);

            var sha256 = SHA256.Create();
            var hashBytes = sha256.ComputeHash(utf8Encoding.GetBytes(body));
            var hash = Convert.ToBase64String(hashBytes);

            if (claimsPrincipal.HasClaim(c => c.Type == "sha256") && claimsPrincipal.FindFirstValue("sha256") != hash)
            {
                throw new ArgumentException("sha256 checksum of body does not match!");
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace _7Assist.Controllers
{
    public class ClaimsController : Controller
    {
        [HttpGet("claims")]
        public IActionResult GetClaims()
        {
            var claims = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

            return Ok(claims);
        }
    }
}

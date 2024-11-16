using Microsoft.AspNetCore.Mvc;

namespace _7Assist.Controllers
{
    public class LiveKitController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Room()
        {
            return View();
        }
        [HttpGet("room2")]
        public IActionResult Room(string roomName)
        {
            ViewBag.RoomName = roomName; 
            return View();
        }
        public IActionResult RoomList()
        {
            return View();
        }
    }
}

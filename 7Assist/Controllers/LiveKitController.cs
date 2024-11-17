using _7Assist.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _7Assist.Controllers
{
    public class LiveKitController : Controller
    {
        private readonly AppDbContext _context;

        public LiveKitController(AppDbContext context) 
        {
            _context = context;
        }
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

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult>RoomList()
        {
            var terminals = _context.Terminals;
            return View(await terminals.ToListAsync());
        }
    }
}

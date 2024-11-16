using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _7Assist.Data;
using _7Assist.Models;
using _7Assist.Services;

namespace _7Assist.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserService _userService;
        private readonly TokenService _tokenService;

        public AccountController(AppDbContext context, UserService userService, TokenService tokenService)
        {
            _context = context;
            _userService = userService;
            _tokenService = tokenService;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("Name, Surname, Patronymic, Login, Password")] User user)
        {
            if (ModelState.IsValid)
            {
                var CreateUser = new User
                {
                    Name = user.Name,
                    Surname = user.Surname,
                    Patronymic = user.Patronymic,
                    Login = user.Login,
                    Password = BCrypt.Net.BCrypt.HashPassword(user.Password),
                };

                _context.Add(CreateUser);
                await _context.SaveChangesAsync();
                return Redirect("~/");
            }
            return Redirect("~/");
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }

        public IActionResult Login()
        {
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> Login([Bind("Login, Password")] User user)
        {

            var userExist = _userService.UserVerify(user);

            if (userExist == null)
            {
                TempData["ErrorMessage"] = "Неверный логин или пароль.";
                return Redirect("~/");
            }

            Response.Cookies.Append("A", _tokenService.CreateToken(userExist));

            return RedirectToAction("Index", "LiveKit");
        }

        [HttpPost]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("A");

            return Redirect("~/");
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using BookLibraryManagementSystem.Models;
using BookLibraryManagementSystem.Data;
using BookLibraryManagementSystem.Helpers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BookLibraryManagementSystem.Controllers
{
    public class LoginAccountController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly PasswordHelper _passwordHelper;

        public LoginAccountController(IUserRepository userRepository, PasswordHelper passwordHelper)
        {
            _userRepository = userRepository;
            _passwordHelper = passwordHelper;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userRepository.GetUserByUsernameAsync(model.Username);
                if (user != null && _passwordHelper.VerifyPassword(user.Password, model.Password))
                {
                    HttpContext.Session.SetString("Username", model.Username);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Invalid username or password.");
            }
            return View(model);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}

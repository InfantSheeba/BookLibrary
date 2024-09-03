using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using BookLibraryManagementSystem.Models;
using BookLibraryManagementSystem.Data;
using System.Threading.Tasks;
using BookLibraryManagementSystem.Helpers;

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
                try
                {
                    // Debug log to check the username being used for lookup
                    System.Diagnostics.Debug.WriteLine($"Username: {model.Username}");

                    var user = await _userRepository.GetUserByUsernameAsync(model.Username);
                    if (user != null)
                    {
                        // Debug log or breakpoint
                        System.Diagnostics.Debug.WriteLine($"Found user: {model.Username}");

                        if (_passwordHelper.VerifyPassword(user.Password, model.Password))
                        {
                            HttpContext.Session.SetString("Username", model.Username);
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    ModelState.AddModelError("", "Invalid username or password.");
                }
                catch (Exception ex)
                {
                    // Log the exception details
                    System.Diagnostics.Debug.WriteLine($"Exception: {ex.Message}");
                    ModelState.AddModelError("", "An error occurred while processing your request.");
                }
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

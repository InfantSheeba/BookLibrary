using Microsoft.AspNetCore.Mvc;
using BookLibraryManagementSystem.Models;
using BookLibraryManagementSystem.Data;
using BookLibraryManagementSystem.Helpers;
using System.Threading.Tasks;

namespace BookLibraryManagementSystem.Controllers
{
    public class RegisterAccountController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly PasswordHelper _passwordHelper;

        public RegisterAccountController(IUserRepository userRepository, PasswordHelper passwordHelper)
        {
            _userRepository = userRepository;
            _passwordHelper = passwordHelper;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
       
        public async Task<IActionResult> Register(BookUsers model)
        {
            if (ModelState.IsValid)
            {
                // Validate that Password and ConfirmPassword match
                if (model.Password != model.ConfirmPassword)
                {
                    ModelState.AddModelError("", "Passwords do not match.");
                    return View(model);
                }

                // Hash password and save user
                model.Password = _passwordHelper.HashPassword(model.Password);

                // Store ConfirmPassword as well if needed
                await _userRepository.RegisterUserAsync(model);
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

    }
}

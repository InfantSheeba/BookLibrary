using BookLibraryManagementSystem.Data;
using BookLibraryManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;


// File: BookLibraryManagementSystem/Controllers/AccountController.cs
using Microsoft.AspNetCore.Mvc;

namespace BookLibraryManagementSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserRepository _userRepository;

        public AccountController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // Example action method to display the registration form
        public IActionResult Register()
        {
            return View();
        }

        // Example action method to handle form submission
        [HttpPost]
        public async Task<IActionResult> Register(Users user)
        {
            if (ModelState.IsValid)
            {
                await _userRepository.CreateUserAsync(user);
                return RedirectToAction("Index", "Home");
            }
            return View(user);
        }
    }
}


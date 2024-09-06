using Microsoft.AspNetCore.Mvc;
using BookLibraryManagementSystem.Models;
using BookLibraryManagementSystem.Data;
using BookLibraryManagementSystem.Helpers;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


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
                // Retrieve user by email
                var user = await _userRepository.GetUserByEmailAsync(model.Email);
                if (user != null && _passwordHelper.VerifyPassword(user.Password, model.Password))
                {
                    // Generate JWT token
                    var token = GenerateJwtToken(user.Email);

                    // Set the JWT token in a cookie
                    HttpContext.Response.Cookies.Append("JwtToken", token, new CookieOptions
                    {
                        HttpOnly = true,
                        Expires = DateTime.UtcNow.AddMinutes(60),
                        Secure = true, // Set to true if using HTTPS
                        SameSite = SameSiteMode.Strict
                    });

                    // Store user details in session (if needed)
                    HttpContext.Session.SetString("Email", user.Email);

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Invalid email or password.");
        
            return View(model);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Response.Cookies.Delete("JwtToken");
            return RedirectToAction("Login");
        }

        private string GenerateJwtToken(string email)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Jwt:Key"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: "JwtIssuer",
                audience: "JwtAudience",
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

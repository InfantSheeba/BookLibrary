using Microsoft.AspNetCore.Identity;

namespace BookLibraryManagementSystem.Helpers
{
    public class PasswordHelper
    {
        private readonly PasswordHasher<object> _passwordHasher = new PasswordHasher<object>();

        /// <summary>
        /// Hashes a plain text password.
        /// </summary>
        /// <param name="password">The plain text password to hash.</param>
        /// <returns>The hashed password.</returns>
        public string HashPassword(string password)
        {
            return _passwordHasher.HashPassword(null, password);
        }

        /// <summary>
        /// Verifies a plain text password against a hashed password.
        /// </summary>
        /// <param name="hashedPassword">The hashed password to verify against.</param>
        /// <param name="providedPassword">The plain text password to verify.</param>
        /// <returns>True if the passwords match; otherwise, false.</returns>
        public bool VerifyPassword(string hashedPassword, string providedPassword)
        {
            var result = _passwordHasher.VerifyHashedPassword(null, hashedPassword, providedPassword);
            return result == PasswordVerificationResult.Success;
        }
    }
}

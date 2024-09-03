using System.ComponentModel.DataAnnotations;

namespace BookLibraryManagementSystem.Models
{
    /// <summary>
    /// This model contains properties for login.
    /// </summary>
    public class LoginViewModel
    {
        // You might not need an Id property for the login view model
        // If not needed, you can remove it
        // public int Id { get; set; } 

        /// <summary>
        /// Gets or sets the username for login.
        /// </summary>
        [Required(ErrorMessage = "Username is required.")]
        [Display(Name = "Username")]
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the password for login.
        /// </summary>
        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}


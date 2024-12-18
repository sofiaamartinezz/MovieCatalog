// lo que le pedimos al usuario para poder registrarse
using System.ComponentModel.DataAnnotations;

namespace MovieCatalog.Models
{
    // Lo que le pedimos al usuario para poder registrarse
    public class RegisterDTO
    {
        [Required(ErrorMessage = "Email is required.")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Invalid email format.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters long.")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[^\w\d\s:]).+$",
            ErrorMessage = "Password must contain at least one uppercase letter, one number, and one special character.")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required.")]
        [Compare("Password", ErrorMessage = "Passwords don't match.")]
        public string? Password2 { get; set; }
    }
}

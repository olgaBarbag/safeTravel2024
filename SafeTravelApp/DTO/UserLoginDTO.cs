using System.ComponentModel.DataAnnotations;

namespace SafeTravelApp.DTO
{
    public class UserLoginDTO
    {
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 50 characters.")]
        public string? Username { get; set; }

        [StringLength(30, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters.")]
        [RegularExpression(@"(?=.*?[A-Z])(?=.*?[a-z])(?=.*?\d)(?=.*?\W)^.{8,}$",
            ErrorMessage = "Password must contain at least one uppercase, one lowercase, " + "one digit, and one special character")]
        public string? Password { get; set; }

        //we let it to Angular form implementation
        //public bool KeepLoggedIn { get; set; }
    }
}

using SafeTravelApp.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace SafeTravelApp.DTO.Agent
{
    public class AgentUpdateDTO
    {
        [Editable(false)]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Username should be between 3 and 50 characters.")]
        public string? Username { get; set; }

        [StringLength(100, ErrorMessage = "Email should not exceed 100 characters.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string? Email { get; set; }

        [StringLength(30, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters.")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*\W).{8,}$",
        ErrorMessage = "Password must contain at least one uppercase letter, " +
            "one lowercase letter, one digit, and one special character.")]
        public string? Password { get; set; }

        [StringLength(50, ErrorMessage = "Firstname should not exceed 50 characters.")]
        public string? Firstname { get; set; }

        [StringLength(50, ErrorMessage = "Lastname should not exceed 50 characters.")]
        public string? Lastname { get; set; }

        [Editable(false)]
        [EnumDataType(typeof(UserRole), ErrorMessage = "Invalid user role.")]
        public UserRole? UserRole { get; set; }
    //--------------------------------------------------------------------------------------------------------------
        [StringLength(15, ErrorMessage = "Phone number should not exceed 15 characters.")]
        [Phone(ErrorMessage = "Invalid phone number.")]
        public string? PhoneNumber { get; set; }

        [StringLength(100, ErrorMessage = "Country should not exceed 100 characters.")]
        public string? Country { get; set; }

        [StringLength(100, ErrorMessage = "City should not exceed 100 characters.")]
        public string? City { get; set; }

        [StringLength(100, ErrorMessage = "Address should not exceed 100 characters.")]
        public string? Address { get; set; }

        [StringLength(6, ErrorMessage = "PostalCode should not exceed 6 characters.")]
        public string? PostalCode { get; set; }
    //--------------------------------------------------------------------------------------------------------------
        [StringLength(100, ErrorMessage = "Company name should not exceed 100 characters.")]
        public string? CompanyName { get; set; }

        [StringLength(10, ErrorMessage = "Vat Number should not exceed 10 characters.")]
        public string? VatNumber { get; set; }
    }
}

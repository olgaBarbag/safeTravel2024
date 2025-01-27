using SafeTravelApp.Core.Enums;

namespace SafeTravelApp.DTO.Citizen
{
    public class CitizenDetailsReadOnlyDTO
    {
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public UserRole? UserRole { get; set; }

        public DateOnly? BirthDate { get; set; }
        public Gender? Gender { get; set; }
        public string? Occupation { get; set; }

        public string? PhoneNumber { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? Address { get; set; }
        public string? PostalCode { get; set; }

        public DateTime InsertedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}

using SafeTravelApp.Core.Enums;

namespace SafeTravelApp.DTO.Agent
{
    public class AgentDetailsReadOnlyDTO
    {
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public UserRole? UserRole { get; set; }
        public string? CompanyName { get; set; }
        public string? VatNumber { get; set; }

        public string? PhoneNumber { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? Address { get; set; }
        public string? PostalCode { get; set; }
    }
}

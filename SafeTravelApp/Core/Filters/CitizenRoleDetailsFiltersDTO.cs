using SafeTravelApp.Core.Enums;

namespace SafeTravelApp.Core.Filters
{
    public class CitizenRoleDetailsFiltersDTO
    {
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? CitizenRole { get; set; }
    }
}

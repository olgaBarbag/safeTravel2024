namespace SafeTravelApp.Core.Filters
{
    public class UserDetaisFiltersDTO
    {
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string Firstname { get; set; } = null!;
        public string Lastname { get; set; } = null!;
        public string? UserRole { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
    }
}

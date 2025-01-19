using SafeTravelApp.Core.Enums;

namespace SafeTravelApp.DTO.Citizen
{
    public class CitizenReadOnlyDTO
    {
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public DateOnly? BirthDate { get; set; }
        public Gender? Gender { get; set; }
        public string? Occupation { get; set; }
        public DateTime InsertedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

    }
}

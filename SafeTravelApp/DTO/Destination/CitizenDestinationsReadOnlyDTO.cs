using SafeTravelApp.Core.Enums;

namespace SafeTravelApp.DTO.Destination
{
    public class CitizenDestinationsReadOnlyDTO
    {
        public string? Country { get; set; } 
        public string? City { get; set; }
        public string? Region { get; set; }
        public DestinationType? Type { get; set; }
        public CitizenRole? CitizenRole { get; set; }
    }
}

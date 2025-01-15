using SafeTravelApp.Core.Enums;

namespace SafeTravelApp.DTO.Destination
{
    public class DestinationReadOnlyDTO
    {
        public int Id { get; set; }
        public string? Country { get; set; } 
        public string? City { get; set; }
        public string? Region { get; set; }
        public DestinationType Type { get; set; }
    }
}

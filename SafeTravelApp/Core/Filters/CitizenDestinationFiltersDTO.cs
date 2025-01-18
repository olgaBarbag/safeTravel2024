using SafeTravelApp.Core.Enums;

namespace SafeTravelApp.Core.Filters
{
    public class CitizenDestinationFiltersDTO
    {        
        public string? Country { get; set; } 
        public string? City { get; set; }
        public string? Region { get; set; }
        public string? Type { get; set; }
        public string? CitizenRole { get; set; }
    }
}

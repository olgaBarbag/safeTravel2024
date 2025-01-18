using SafeTravelApp.Core.Enums;

namespace SafeTravelApp.DTO.Recommendation
{
    public class DestinationRecommendationReadOnlyDTO
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public ContributorRole? ContributorRole { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? Region { get; set; }
        public DestinationType? Type { get; set; }
        public DestinationCategory? DestinationCategory { get; set; }
    }
}

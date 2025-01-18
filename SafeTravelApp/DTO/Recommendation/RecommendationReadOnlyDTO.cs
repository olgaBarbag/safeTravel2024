using SafeTravelApp.Core.Enums;

namespace SafeTravelApp.DTO.Recommendation
{
    public class RecommendationReadOnlyDTO
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public ContributorRole? ContributorRole { get; set; }
        public int? ContributorId { get; set; } 
        public int? CategoryId { get; set; }
        public int? DestinationId { get; set; }
    }
}

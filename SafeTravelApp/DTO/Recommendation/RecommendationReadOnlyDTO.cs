using SafeTravelApp.Core.Enums;

namespace SafeTravelApp.DTO.Recommendation
{
    public class RecommendationReadOnlyDTO
    {
        public int Id { get; set; }
        public string? Title { get; set; } = null!;
        public string? Description { get; init; }
        public ContributorRole? ContributorRole { get; set; }
        public int? ContributorId { get; set; } 
        public int? CategoryId { get; set; }
        public int? DestinationId { get; set; }
    }
}

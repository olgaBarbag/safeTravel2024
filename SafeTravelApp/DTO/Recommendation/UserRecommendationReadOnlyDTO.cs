using SafeTravelApp.Core.Enums;

namespace SafeTravelApp.DTO.Recommendation
{
    public class UserRecommendationReadOnlyDTO
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public ContributorRole? ContributorRole { get; set; }
        public int? ContributorId { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
    }
}

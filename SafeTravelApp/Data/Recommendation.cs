using SafeTravelApp.Core.Enums;

namespace SafeTravelApp.Data
{
    public class Recommendation
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; init; }
        public ContributorRole? ContributorRole { get; set; }
        public int ContributorId { get; set; } //userId
        public int CategoryId { get; set; }
        public int DestinationId { get; set; }

        public virtual Category Category { get; set; } = null!;
        public virtual User User { get; set; } = null!;
        public virtual Destination Destination { get; set; } = null!;
    }
}


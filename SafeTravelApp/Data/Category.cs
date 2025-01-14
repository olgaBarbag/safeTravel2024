using SafeTravelApp.Core.Enums;

namespace SafeTravelApp.Data
{
    public class Category
    {
        public int Id { get; set; }
        public DestinationCategory? DestinationCategory { get; set; }
        public string Description { get; set; } = null!;
        
        public virtual ICollection<Destination>? Destinations { get; set; } = new HashSet<Destination>();
        public virtual ICollection<Recommendation>? Recommendations { get; set; } = new HashSet<Recommendation>();
    }
}

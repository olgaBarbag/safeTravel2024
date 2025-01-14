using SafeTravelApp.Core.Enums;

namespace SafeTravelApp.Data
{
    public class Destination 
    {
        public int Id { get; set; }
        public string Country { get; set; } = null!;
        public string? City { get; set; }
        public string? Region { get; set; }
        public DestinationType Type { get; set; }
       
        //Nullable: If category deleted then destination null
        public virtual ICollection<Category>? Categories { get; set; } = new HashSet<Category>();
        public virtual ICollection<Agent>? Agents { get; set; } = new HashSet<Agent>();
        public virtual ICollection<Citizen>? Citizens { get; set; } = new HashSet<Citizen>();
        public virtual ICollection<CitizenDestination>? CitizenDestinations { get; set;} = new HashSet<CitizenDestination>();
        public virtual ICollection<Recommendation>? Recommendations { get; set; } = new HashSet<Recommendation>();
    }
}

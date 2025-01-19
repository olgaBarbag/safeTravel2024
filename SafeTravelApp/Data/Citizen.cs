using SafeTravelApp.Core.Enums;

namespace SafeTravelApp.Data
{
    public class Citizen : BaseEntity
    {
        public int Id { get; set; }
        public DateOnly? BirthDate { get; set; } 
        public Gender? Gender { get; set; } 
        public string? Occupation { get; set; }
        public int UserId { get; set; }

        //Non - nullable: If user deleted then citizen too
        public virtual User User { get; set; } = null!;
        public ICollection<CitizenDestination>? CitizenDestinations { get; set; } = new HashSet<CitizenDestination>();
        public virtual ICollection<Destination>? Destinations { get; set; } = new HashSet<Destination>();
    }
}

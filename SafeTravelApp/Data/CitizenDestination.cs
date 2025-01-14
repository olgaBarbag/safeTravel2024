using SafeTravelApp.Core.Enums;

namespace SafeTravelApp.Data
{
    public class CitizenDestination : BaseEntity
    {
        public CitizenRole? CitizenRole { get; set; }
        public int CitizenId { get; set; }
        public int DestinationId { get; set; }


        public virtual Citizen Citizen { get; set; } = null!;
        public virtual Destination Destination { get; set; } = null!;
    }
}

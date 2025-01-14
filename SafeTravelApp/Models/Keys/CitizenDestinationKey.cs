namespace SafeTravelApp.Models.Keys
{
    public class CitizenDestinationKey
    {
        public int CitizenId { get; set; }
        public int DestinationId { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is CitizenDestinationKey other &&
                   CitizenId == other.CitizenId &&
                   DestinationId == other.DestinationId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(CitizenId, DestinationId);
        }
    }
}

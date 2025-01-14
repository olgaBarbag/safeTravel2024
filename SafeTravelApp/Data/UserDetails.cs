namespace SafeTravelApp.Data
{
    public class UserDetails : BaseEntity
    {
        public string PhoneNumber { get; set; } = null!; //unique
        public string Country { get; set; } = null!;
        public string City { get; set; } = null!;
        public string? Address { get; set; }
        public string? PostalCode { get; set; }
        public int UserId { get; set; } 

        //Non - nullable: If user deleted then details too
        public virtual User User { get; set; } = null!;
    }
}

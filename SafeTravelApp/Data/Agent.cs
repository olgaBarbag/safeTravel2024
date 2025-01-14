namespace SafeTravelApp.Data
{
    public class Agent : BaseEntity
    {
        public int Id { get; set; }
        public string CompanyName { get; set; } = null!;
        public string VatNumber { get; set; } = null!;  //unique
        // public string phoneNumber { get; set; }
        public int UserId { get; set; }

        //Non - nullable: If user deleted then agent too.
        public virtual User User { get; set; } = null!;

        //Nullable: If certifications deleted then agent null
        public virtual ICollection<Certification>? Certifications { get; set; } = new HashSet<Certification>();
        public virtual ICollection<Language>? Languages { get; set; } = new HashSet<Language>();
        public virtual ICollection<Destination>? Destinations { get; set; } = new HashSet<Destination>();
        
    }
}

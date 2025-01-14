namespace SafeTravelApp.Data
{
    public class Certification
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public string? IssuedBy { get; set; }
        public DateTime? IssueDate { get; set; }
        public DateTime? ExpiryDate { get; set;}
        public int AgentId { get; set; }

        //Non - nullable: If agent deleted then certification too.
        public virtual Agent Agent { get; set; } = null!;

    }
}

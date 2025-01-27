namespace SafeTravelApp.DTO.Agent
{
    public class AgentReadOnlyDTO
    {        
        public string? Username { get; set; }
        public string? Email { get; set; }    
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }

        public string? CompanyName { get; set; }
        public string? VatNumber { get; set; }

        public DateTime InsertedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

    }
}

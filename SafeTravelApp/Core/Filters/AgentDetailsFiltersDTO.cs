namespace SafeTravelApp.Core.Filters
{
    public class AgentDetailsFiltersDTO
    {
        public int AgentId { get; set; }
        public string? CompanyName { get; set; }
        public string? VatNumber {  get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
    }
}

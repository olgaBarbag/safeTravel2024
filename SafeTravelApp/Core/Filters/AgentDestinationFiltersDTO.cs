﻿using SafeTravelApp.Core.Enums;

namespace SafeTravelApp.Core.Filters
{
    public class AgentDestinationFiltersDTO
    {
        public string? CompanyName { get; set; }
        public string? VatNumber { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string Country { get; set; } = null!;
        public string? City { get; set; }
        public DestinationType? Type { get; set; }
    }
}

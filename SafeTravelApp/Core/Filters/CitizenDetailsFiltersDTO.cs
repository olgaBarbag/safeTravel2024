﻿namespace SafeTravelApp.Core.Filters
{
    public class CitizenDetailsFiltersDTO
    {
        public int CitizenId { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
    }
}

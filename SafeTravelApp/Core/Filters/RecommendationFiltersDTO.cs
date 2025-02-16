﻿using SafeTravelApp.Core.Enums;

namespace SafeTravelApp.Core.Filters
{
    public class RecommendationFiltersDTO
    {
        public string? Title { get; set; }
        public string? DestinationCategory { get; set; }
        public string? Country { get; set; } = null!;
        public string? City { get; set; }
    }
}

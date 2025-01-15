﻿using SafeTravelApp.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace SafeTravelApp.DTO.Destination
{
    public class DestinationUpdateDTO
    {
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Country should be between 3 and 50 characters.")]
        public string? Country { get; set; }

        [StringLength(50, MinimumLength = 3, ErrorMessage = "City should be between 3 and 50 characters.")]
        public string? City { get; set; }

        [StringLength(50, MinimumLength = 3, ErrorMessage = "Region should be between 3 and 50 characters.")]
        public string? Region { get; set; }

        [EnumDataType(typeof(DestinationType), ErrorMessage = "Invalid destination type.")]
        public DestinationType Type { get; set; }
    }
}

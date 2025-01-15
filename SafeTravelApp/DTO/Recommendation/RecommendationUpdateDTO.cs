using SafeTravelApp.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace SafeTravelApp.DTO.Recommendation
{
    public class RecommendationUpdateDTO
    {
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Country should be between 3 and 50 characters.")]
        public string? Country { get; set; }
               
        [StringLength(50, MinimumLength = 3, ErrorMessage = "City should be between 3 and 50 characters.")]
        public string? City { get; set; }
                
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Region should be between 3 and 50 characters.")]
        public string? Region { get; set; }

        [EnumDataType(typeof(DestinationType), ErrorMessage = "Invalid destination type.")]
        public DestinationType Type { get; set; }

        [EnumDataType(typeof(DestinationType), ErrorMessage = "Invalid category.")]
        public DestinationCategory? DestinationCategory { get; set; }

        [StringLength(100, ErrorMessage = "Email should not exceed 100 characters.")]
        public string? Title { get; set; }

        [StringLength(100, ErrorMessage = "Email should not exceed 100 characters.")]
        public string? Description { get; init; }

        [EnumDataType(typeof(ContributorRole), ErrorMessage = "Invalid contributor role.")]
        public ContributorRole? ContributorRole { get; set; }


    }
}

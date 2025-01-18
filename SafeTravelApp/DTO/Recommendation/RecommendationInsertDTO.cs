using SafeTravelApp.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace SafeTravelApp.DTO.Recommendation
{
    public class RecommendationInsertDTO
    {
        //[Required(ErrorMessage = "The {0} field is required.")]
        //[StringLength(50, MinimumLength = 3, ErrorMessage = "Country should be between 3 and 50 characters.")]
        //public string? Country { get; set; }

        //[StringLength(50, MinimumLength = 3, ErrorMessage = "City should be between 3 and 50 characters.")]
        //public string? City { get; set; }

        //[StringLength(50, MinimumLength = 3, ErrorMessage = "Region should be between 3 and 50 characters.")]
        //public string? Region { get; set; }

        //[Required(ErrorMessage = "The {0} field is required.")]
        //[EnumDataType(typeof(DestinationType), ErrorMessage = "Invalid destination type.")]
        //public DestinationType? Type { get; set; }

        //[Required(ErrorMessage = "The {0} field is required.")]
        //[EnumDataType(typeof(DestinationCategory), ErrorMessage = "Invalid category.")]
        //public DestinationCategory? DestinationCategory { get; set; }

        public int DestinationId { get; set; }

        public int CategoryId { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [StringLength(100, ErrorMessage = "Title should not exceed 100 characters.")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [StringLength(100, ErrorMessage = "Description should not exceed 100 characters.")]
        public string? Description { get; init; }

        //[Required(ErrorMessage = "The {0} field is required.")]
        //[EnumDataType(typeof(ContributorRole), ErrorMessage = "Invalid contributor role.")]
        //public ContributorRole? ContributorRole { get; set; }
                
        
    }
}

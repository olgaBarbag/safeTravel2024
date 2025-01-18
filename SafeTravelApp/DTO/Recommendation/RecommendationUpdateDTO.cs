using SafeTravelApp.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace SafeTravelApp.DTO.Recommendation
{
    public class RecommendationUpdateDTO
    {       
        [StringLength(100, ErrorMessage = "Title should not exceed 100 characters.")]
        public string? Title { get; set; }

        [StringLength(100, ErrorMessage = "Description should not exceed 100 characters.")]
        public string? Description { get; init; }

    }
}

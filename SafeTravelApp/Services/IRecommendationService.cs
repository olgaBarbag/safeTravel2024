using SafeTravelApp.Core.Filters;
using SafeTravelApp.Data;
using SafeTravelApp.DTO.Recommendation;
using SafeTravelApp.DTO.User;

namespace SafeTravelApp.Services
{
    public interface IRecommendationService
    {
        Task<Recommendation?> InsertRecommendationAsync(int userId, RecommendationInsertDTO insertDTO);
        Task<Recommendation?> UpdateRecommendationAsync(int userId, int recommendationId, RecommendationUpdateDTO updateDTO);
        Task DeleteRecommendationAsync(int id);

        Task<List<DestinationRecommendationReadOnlyDTO>> GetRecommendationsByContributorIdAsync(int contributorId);
        Task<List<DestinationRecommendationReadOnlyDTO>> GetRecommendationsByDestinationAsync(RecommendationFiltersDTO recommendationFiltersDTO);


    }
}

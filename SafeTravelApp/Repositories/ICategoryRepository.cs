using SafeTravelApp.Core.Enums;
using SafeTravelApp.Data;

namespace SafeTravelApp.Repositories
{
    public interface ICategoryRepository
    {
        Task<List<Recommendation>> GetRecommendationsByDestinationAndCategoryAsync(int destinationId, DestinationCategory destinationCategory);
        Task<List<Agent>> GetAgentsByDestinationAndCategoryAsync(int destinationId, DestinationCategory destinationCategory);
        Task<List<Citizen>> GetCitizensByDestinationAndCategoryAsync(int destinationId, DestinationCategory destinationCategory);

        Task<int> CountAgentsByDestinationAndCategoryAsync(int destId, DestinationCategory destCategory);
        Task<int> CountCitizensByDestinationAndCategoryAsync(int destId, DestinationCategory destCategory);
    }
}

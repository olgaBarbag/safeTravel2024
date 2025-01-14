using SafeTravelApp.Core.Enums;
using SafeTravelApp.Data;

namespace SafeTravelApp.Repositories
{
    public interface IRecommendationRepository 
    {

        Task<List<Recommendation>> GetRecommendationsByDestinationFiltered(List<Func<Destination, bool>> predicates);
        Task<List<Recommendation>> GetRecommendationsByContributorId(int contributorId);
        Task<List<Recommendation>> GetRecommendationsByDestinationAndContributorType(int destId, ContributorRole role);
        //Task SetContributorRoleAsync(int recommendationId, int contributorId);
    }
}

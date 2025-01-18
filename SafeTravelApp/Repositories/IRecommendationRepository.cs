using SafeTravelApp.Core.Enums;
using SafeTravelApp.Data;
using System.Threading.Tasks;

namespace SafeTravelApp.Repositories
{
    public interface IRecommendationRepository 
    {

        Task<List<Recommendation>> GetRecommendationsByDestinationFiltered(List<Func<Destination, bool>> predicates);
        Task<List<Recommendation>> GetRecommendationsByContributorId(int contributorId);
        Task<List<Recommendation>> GetRecommendationsByDestinationAndContributorType(int destId, ContributorRole role);
        Task<ContributorRole> GetContributorRole(User user, Destination destination);
        Task<Recommendation?> UpdateRecommendationAsync(int userId, Recommendation recommendation);
    }
}

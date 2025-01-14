using SafeTravelApp.Core.Enums;
using SafeTravelApp.Data;
using System.Threading.Tasks;

namespace SafeTravelApp.Repositories
{
    public interface IDestinationRepository
    {
        
        Task<List<Agent>> GetAgentsByDestinationAsync(int destinationId);
        Task<List<Citizen>> GetCitizensByDestinationAsync(int destinationId);
        Task<List<Recommendation>> GetRecommendationsByDestinationAsync(int destinationId);

        Task<List<Destination>> GetDestinationsByTypeAsync(DestinationType type);
        //Task<List<Agent>> GetAgentsByTypeAsync(DestinationType type);
        //Task<List<Citizen>> GetCitizenByTypeAsync(DestinationType type);

        
    }
}

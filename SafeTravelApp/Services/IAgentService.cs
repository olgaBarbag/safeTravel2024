using SafeTravelApp.Core.Enums;
using SafeTravelApp.Core.Filters;
using SafeTravelApp.Data;
using SafeTravelApp.DTO.Agent;
using SafeTravelApp.DTO.Citizen;
using SafeTravelApp.DTO.Destination;
using SafeTravelApp.DTO.User;

namespace SafeTravelApp.Services
{
    public interface IAgentService
    {
        Task<AgentDetailsReadOnlyDTO> SignUpUserAsync(AgentSignUpDTO request);
        Task<List<AgentReadOnlyDTO>> GetAllUsersAgentsAsync(int pageNumber, int pageSize);
        Task<List<AgentReadOnlyDTO>> GetAllUsersAgentsFilteredAsync(AgentDetailsFiltersDTO agentDetailsFiltersDTO);

        Task<AgentDetailsReadOnlyDTO?> GetAgentByUsernameAsync(string username);
        Task<AgentDetailsReadOnlyDTO?> GetAgentByIdAsync(int id);
        Task<AgentDetailsReadOnlyDTO?> GetAgentByPhoneNumberAsync(string phoneNumber);

        Task<List<Destination>> GetAllAgentDestinationsFilteredAsync(Agent agent, DestinationFiltersDTO destinationFiltersDTO); 
        Task<List<AgentReadOnlyDTO>> GetAllDestinationAgentsFilteredAsync(Destination destination, AgentDetailsFiltersDTO agentDetailsFiltersDTO);

        Task<List<Language>> GetAllAgentLanguagesFilteredAsync(Agent agent, LanguageFiltersDTO languageFiltersDTO);
        Task<List<Certification>> GetAllAgentCertificationsAsync(Agent agent);

        //Task<List<Recommendation>> GetAllAgentRecommendationsFilteredAsync(int id, Recommendation recommendation);

    }
}

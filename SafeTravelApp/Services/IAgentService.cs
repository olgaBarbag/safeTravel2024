using SafeTravelApp.Core.Filters;
using SafeTravelApp.Data;
using SafeTravelApp.DTO.Agent;
using SafeTravelApp.DTO.User;

namespace SafeTravelApp.Services
{
    public interface IAgentService
    {
        Task<UserReadOnlyDTO> SignUpUserAsync(AgentSignUpDTO request);
        Task<List<User>> GetAllUsersAgentsAsync();
        Task<List<User>> GetAllUsersAgentsFilteredAsync(UserDetaisFiltersDTO userDetaisFiltersDTO);
        Task<AgentReadOnlyDTO?> GetAgentByUsernameAsync(string username);
    }
}

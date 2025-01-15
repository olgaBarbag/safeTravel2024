using SafeTravelApp.Core.Filters;
using SafeTravelApp.Data;
using SafeTravelApp.DTO.Agent;
using SafeTravelApp.DTO.Citizen;
using SafeTravelApp.DTO.User;

namespace SafeTravelApp.Services
{
    public interface ICitizenService
    {
        Task<UserReadOnlyDTO> SignUpUserAsync(CitizenSignUpDTO request);
        Task<List<User>> GetAllUsersCitizensAsync();
        Task<List<User>> GetAllUsersCitizensFilteredAsync(UserDetaisFiltersDTO userDetaisFiltersDTO);
        Task<AgentReadOnlyDTO?> GetCitizenByUsernameAsync(string username);
    }
}

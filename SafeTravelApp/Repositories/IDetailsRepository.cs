using SafeTravelApp.Core.Filters;
using SafeTravelApp.Data;

namespace SafeTravelApp.Repositories
{
    public interface IDetailsRepository
    {
        Task<List<User>?> GetAllUsersWithDetailsAsync(); //All users + details
        Task<List<User>?> GetDetailsAllUsersAgents();    //All users.Agents + details
        Task<List<User>?> GetDetailsAllUsersCitizens();  //All users.Citizens + details
        Task<List<UserDetaisFiltersDTO>?> GetDetailsUsersFiltered(List<Func<UserDetails, bool>> predicates); //All users + details.filtered
        Task<List<AgentDetailsFiltersDTO>?> GetDetailsAllUsersAgentsFiltered(List<Func<UserDetails, bool>> predicates); //All users.Agents + details.filtered
        Task<List<CitizenDetailsFiltersDTO>?> GetDetailsAllUsersCitizensFiltered(List<Func<UserDetails, bool>> predicates);  //All users.Citizens + details.filtered

        Task<UserDetails?> UpdateUserDetailsAsync(int id, UserDetails userDetails);
    }
}

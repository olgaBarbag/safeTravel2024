using SafeTravelApp.Data;
using SafeTravelApp.Models;

namespace SafeTravelApp.Repositories
{
    public interface IAgentRepository
    {
        //Task<List<Destination>> GetAgentDestinationsAsync(int id);
        Task<List<Destination>> GetAgentDestinationsFilteredAsync(int agentId, List<Func<Destination, bool>> predicates);
        Task<List<Certification>> GetAgentCertificationsFilteredAsync(int agentId, List<Func<Certification, bool>> predicates);
        Task<List<Language>> GetAgentLanguagesFilteredAsync(int agentId, List<Func<Language, bool>> predicates);

        Task<List<Agent>> GetAgentsUserFilteredAsync(List<Func<User, bool>> predicates);               //AgentFiltersDTO
        Task<List<Agent>> GetAgentsDestinationFilteredAsync(List<Func<Destination, bool>> predicates); //AgentDestinationFiltersDTO
        Task<List<Agent>> GetAgentsLanguageFilteredAsync(List<Func<Language, bool>> predicates);       //AgentFiltersDTO

        // Task<User?> GetByPhoneNumberAsync(string phoneNumber); It is implemented: DetailsRepo -> GetDetailsAllUsersAgentsFiltered 
        Task<User?> GetUserAgentByUsernameAsync(string username);                                      //AgentFiltersDTO

        Task<List<User>?> GetAllUsersAgentsAsync();                                                     //AgentFiltersDTO
        Task<List<User>?> GetAllUsersAgentsPaginatedAsync(int pageNumber, int pageSize);                //AgentFiltersDTO

        Task<PaginatedResult<User>> GetUsersAgentsPagedFilteredAsync(int pageNumber, int pageSize,     
            List<Func<User, bool>> predicates);
    }
}

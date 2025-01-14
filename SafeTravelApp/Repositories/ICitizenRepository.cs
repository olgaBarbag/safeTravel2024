using SafeTravelApp.Core.Enums;
using SafeTravelApp.Data;
using SafeTravelApp.Models;

namespace SafeTravelApp.Repositories
{
    public interface ICitizenRepository
    {
        //Task<List<Destination>> GetAgentDestinationsAsync(int id);
        Task<List<Destination>> GetCitizenDestinationsFilteredAsync(int id, List<Func<Destination, bool>> predicates);
        Task<List<CitizenDestination>> GetCitzenDestinationsFilteredAsync(int id, List<Func<CitizenDestination, bool>> predicates);
        
        Task<List<Citizen>> GetCitizensUserFilteredAsync(List<Func<User, bool>> predicates);               //CitizenFiltersDTO
        Task<List<Citizen>> GetCitizensDestinationFilteredAsync(List<Func<Destination, bool>> predicates); //CitizenDestinationFiltersDTO
        Task<List<Citizen>> GetCitizensDestinationFilteredByCitizenRoleAsync(CitizenRole citizenRole, List<Func<Destination, bool>> predicates);

        Task<User?> GetUserCitizenByUsernameAsync(string username);                                      //CitizenFiltersDTO

        Task<List<User>?> GetAllUsersCitizensAsync();                                                     //CitizenFiltersDTO
        Task<List<User>?> GetAllUsersCitizensPaginatedAsync(int pageNumber, int pageSize);                //CitizenFiltersDTO
        Task<PaginatedResult<User>> GetUsersCitizensPagedFilteredAsync(int pageNumber, int pageSize,
            List<Func<User, bool>> predicates);
    }
}

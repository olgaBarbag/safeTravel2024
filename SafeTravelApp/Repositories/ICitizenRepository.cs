using SafeTravelApp.Core.Enums;
using SafeTravelApp.Data;
using SafeTravelApp.Models;

namespace SafeTravelApp.Repositories
{
    public interface ICitizenRepository
    {
        //Task<List<Destination>> GetAgentDestinationsAsync(int id);
        Task<List<Destination>> GetCitizenDestinationsFilteredAsync(int id, List<Func<Destination, bool>> predicates);
                            
        Task<List<Citizen>?> GetCitizensDestinationFilteredAsync(int id, List<Func<Citizen, bool>> predicates); 
        
        Task<User?> GetUserCitizenByUsernameAsync(string username);                                      
        Task<User?> GetUserCitizenByPhoneNumberAsync(string phoneNumber);

        Task<List<Citizen>?> GetUserCitizensFilteredAsync(List<Func<User, bool>> predicates);
        Task<List<User>?> GetAllUsersCitizensAsync();                                                     
        Task<List<User>?> GetAllUsersCitizensPaginatedAsync(int pageNumber, int pageSize);                
        Task<PaginatedResult<User>> GetUsersCitizensPagedFilteredAsync(int pageNumber, int pageSize,
            List<Func<User, bool>> predicates);
    }
}

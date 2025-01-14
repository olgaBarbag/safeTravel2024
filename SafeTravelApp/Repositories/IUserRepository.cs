using SafeTravelApp.Data;
using SafeTravelApp.Models;

namespace SafeTravelApp.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetUserAsync(string username, string password);
        
        Task<User?> GetByUsernameAsync(string username);
        Task<User?> GetByPhoneNumberAsync(string phoneNumber);
        Task<List<User>?> GetAllUsersFilteredPaginatedAsync(int pageNumber, int pageSize,
            List<Func<User, bool>> predicates);
        Task<PaginatedResult<User>> GetAllUsersPagedAsync(int pageNumber, int pageSize,
            List<Func<User, bool>> predicates);
        Task<User?> UpdateUserAsync(int id, User user);
    }
}

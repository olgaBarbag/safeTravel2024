using SafeTravelApp.Core.Filters;
using SafeTravelApp.Data;
using SafeTravelApp.DTO.User;

namespace SafeTravelApp.Services
{
    public interface IUserService
    {
        Task<User?> VerifyAndGetUserAsync(UserLoginDTO credentials);
        Task<User?> GetUserByUsernameAsync(string username);
        Task<List<User>> GetAllUsersFiltered(int pageNumber, int pageSize,
            UserFiltersDTO userFiltersDTO);
        
        Task<User?> GetUserByIdAsync(int id);
    }
}

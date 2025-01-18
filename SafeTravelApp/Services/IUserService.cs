using SafeTravelApp.Core.Filters;
using SafeTravelApp.Data;
using SafeTravelApp.DTO.User;

namespace SafeTravelApp.Services
{
    public interface IUserService
    {
        Task<User?> VerifyAndGetUserAsync(UserLoginDTO credentials);
        Task<User?> SignUpUserAsync(UserSignupDTO signupDTO);
        Task<User?> UpdateUserAsync(int userId, UserUpdateDTO userUpdateDTO);
        Task<User?> GetUserByUsernameAsync(string username);
        Task<User?> GetUserByPhoneNumberAsync(string phoneNumber);
        Task<User?> GetUserByIdAsync(int id);
        Task<List<User>?> GetAllUsersFilteredAsync(int pageNumber, int pageSize,
            UserFiltersDTO userFiltersDTO);
        Task<List<User>> GetAllUsersFiltered(int pageNumber, int pageSize,
            UserDetailsFiltersDTO userDetailsFiltersDTO);



    }
}

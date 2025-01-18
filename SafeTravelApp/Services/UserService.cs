using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using SafeTravelApp.Core.Enums;
using SafeTravelApp.Core.Filters;
using SafeTravelApp.Data;
using SafeTravelApp.DTO.User;
using SafeTravelApp.Exceptions;
using SafeTravelApp.Repositories;
using SafeTravelApp.Security;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SafeTravelApp.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;


        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = new LoggerFactory().AddSerilog().CreateLogger<UserService>();
        }

        public async Task<User?> VerifyAndGetUserAsync(UserLoginDTO credentials)
        {
            User? user;

            try
            {
                user = await _unitOfWork.UserRepository.GetUserAsync(credentials.Username!, credentials.Password!);
                if (user != null)
                {
                    _logger.LogInformation("{Message}", "User: " + user!.Username + " found and retrieved.");   // ToDo toString()
                }

            }
            catch (Exception ex)
            {
                _logger.LogError("{Message}{Exception}", ex.Message, ex.StackTrace);
                throw;
            }
            return user;
        }



        public async Task<User?> SignUpUserAsync(UserSignupDTO signupDTO)
        {
            User? user;

            try
            {
                user = _mapper!.Map<User>(signupDTO);

                User? existingUser = await _unitOfWork!.UserRepository.GetByUsernameAsync(user.Username!);

                if (existingUser is not null)
                {
                    throw new EntityAlreadyExistsException("User", $"User with username {user.Username} already exists.");
                }

                user.Password = EncryptionUtil.Encrypt(user.Password!);
                await _unitOfWork!.UserRepository.AddAsync(user);

                await _unitOfWork.SaveAsync();
                _logger!.LogInformation("{Message}", "User " + user.Username + "has signed up successfully.");
            }
            catch (Exception e)
            {
                _logger!.LogError("{Message}{Exception}", e.Message, e.StackTrace);
                throw;
            }
            return user;
        }

        public async Task<User?> UpdateUserAsync(int userId, UserUpdateDTO userUpdateDTO)
        {
            User userToUpdate;

            try
            {
                userToUpdate = _mapper!.Map<User>(userUpdateDTO);

                //ο ελεγχος γινεται στο repo
                await _unitOfWork.UserRepository.UpdateUserAsync(userId, userToUpdate);
                await _unitOfWork.SaveAsync();
                _logger!.LogInformation("{Message}", "User: " + userUpdateDTO.Username + " updated successfully");

            }
            catch (Exception e)
            {
                _logger!.LogError("{Message}{Exception}", e.Message, e.StackTrace);
                throw;
            }

            return userToUpdate;

        }

        public async Task DeleteUserAsync(int id)
        {
            bool deleted;
            try
            {

                deleted = await _unitOfWork!.UserRepository.DeleteAsync(id);

                if (!deleted)
                {
                    throw new EntityNotFoundException("User", "User was not found");
                }

                await _unitOfWork.SaveAsync();
                _logger!.LogInformation("{Message}", "User with id: " + id + " deleted successfully");

            }
            catch (Exception e)
            {
                _logger!.LogError("{Message}{Exception}", e.Message, e.StackTrace);
                throw;
            }
        }
        
        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            User? user = null;

            try
            {
                user = await _unitOfWork.UserRepository.GetByUsernameAsync(username);
                _logger!.LogInformation("{Message}", "User: " + username + " was found successfully.");
            }
            catch (Exception e)
            {
                _logger.LogError("{Message}{Excpetion}", e.Message, e.StackTrace);
            }
            return user;
        }


        public async Task<User?> GetUserByPhoneNumberAsync(string phoneNumber)
        {
            User? user = null; ;

            try
            {
                user = await _unitOfWork.UserRepository.GetByPhoneNumberAsync(phoneNumber);
                _logger!.LogInformation("{Message}", "User: " + phoneNumber + " was found successfully.");
            }
            catch (Exception e)
            {
                _logger.LogError("{Message}{Excpetion}", e.Message, e.StackTrace);
            }
            return user;
        }



        public async Task<User?> GetUserByIdAsync(int id)
        {
            User? user = null;

            try
            {
                user = await _unitOfWork.UserRepository.GetByIdAsync(id);
                _logger!.LogInformation("{Message}", "The user with userId: " + id + " was found successfully.");
            }
            catch (Exception e)
            {
                _logger.LogError("{Message}{Excpetion}", e.Message, e.StackTrace);
            }
            return user;
        }

        public async Task<List<User>> GetAllUsersFilteredAsync(int pageNumber, int pageSize,
            UserFiltersDTO userFiltersDTO)
        {
            List<User> users;
            List<Func<User, bool>> predicates = new();

            try
            {
                if (!string.IsNullOrEmpty(userFiltersDTO.Username))
                {
                    predicates.Add(u => u.Username == userFiltersDTO.Username);
                }
                if (!string.IsNullOrEmpty(userFiltersDTO.Email))
                {
                    predicates.Add(u => u.Email == userFiltersDTO.Email);
                }
                if (!string.IsNullOrEmpty(userFiltersDTO.Firstname))
                {
                    predicates.Add(u => u.Firstname == userFiltersDTO.Firstname);
                }
                if (!string.IsNullOrEmpty(userFiltersDTO.Lastname))
                {
                    predicates.Add(u => u.Lastname == userFiltersDTO.Lastname);
                }
                if (!string.IsNullOrEmpty(userFiltersDTO.UserRole))
                {
                    predicates.Add(u => u.UserRole.ToString() == userFiltersDTO.UserRole);
                }

                users = await _unitOfWork.UserRepository
                    .GetAllUsersFilteredPaginatedAsync(pageNumber, pageSize, predicates);
            }
            catch (Exception e)
            {
                _logger.LogError("{Message}{Exception}", e.Message, e.StackTrace);
                throw;
            }
            return users;
        }

        public async Task<List<User>> GetAllUsersFiltered(int pageNumber, int pageSize, UserDetailsFiltersDTO userDetailsFiltersDTO)
        {
            List<User> users;
            List<Func<User, bool>> predicates = new();

            try
            {
                if (!string.IsNullOrEmpty(userDetailsFiltersDTO.Username))
                {
                    predicates.Add(u => u.Username == userDetailsFiltersDTO.Username);
                }
                if (!string.IsNullOrEmpty(userDetailsFiltersDTO.Email))
                {
                    predicates.Add(u => u.Email == userDetailsFiltersDTO.Email);
                }
                if (!string.IsNullOrEmpty(userDetailsFiltersDTO.Firstname))
                {
                    predicates.Add(u => u.Firstname == userDetailsFiltersDTO.Firstname);
                }
                if (!string.IsNullOrEmpty(userDetailsFiltersDTO.Lastname))
                {
                    predicates.Add(u => u.Lastname == userDetailsFiltersDTO.Lastname);
                }
                if (!string.IsNullOrEmpty(userDetailsFiltersDTO.UserRole))
                {
                    predicates.Add(u => u.UserRole.ToString() == userDetailsFiltersDTO.UserRole);
                }
                if (!string.IsNullOrEmpty(userDetailsFiltersDTO.PhoneNumber))
                {
                    predicates.Add(u => u.Details!.PhoneNumber == userDetailsFiltersDTO.PhoneNumber); 
                }
                if (!string.IsNullOrEmpty(userDetailsFiltersDTO.Address))
                {
                    predicates.Add(u => u.Details!.Address == userDetailsFiltersDTO.Address);
                }
                if (!string.IsNullOrEmpty(userDetailsFiltersDTO.City))
                {
                    predicates.Add(u => u.Details!.City == userDetailsFiltersDTO.City);
                }

                users = await _unitOfWork.UserRepository
                    .GetAllUsersFilteredPaginatedAsync(pageNumber, pageSize, predicates);
            }
            catch (Exception e)
            {
                _logger.LogError("{Message}{Exception}", e.Message, e.StackTrace);
                throw;
            }
            return users;
        }

        public string CreateUserToken(int userId, string username, string email, UserRole? userRole,
            string appSecurityKey)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(appSecurityKey));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claimsInfo = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, userRole.ToString()!)
            };

            var issuer = "https://localhost:5000";
            var audience = "https://localhost:4200";

            var jwtSecurityToken = new JwtSecurityToken(issuer, audience, claimsInfo, DateTime.UtcNow,
                DateTime.UtcNow.AddHours(3), signingCredentials);
            //var jwtSecurityToken = new JwtSecurityToken(null, null, claimsInfo, DateTime.UtcNow,
            //    DateTime.UtcNow.AddHours(3), signingCredentials);

            // Serialize the token
            var userToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return userToken;
        }
       
    }
}

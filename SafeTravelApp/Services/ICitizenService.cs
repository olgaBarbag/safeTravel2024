using SafeTravelApp.Core.Enums;
using SafeTravelApp.Core.Filters;
using SafeTravelApp.Data;
using SafeTravelApp.DTO.Agent;
using SafeTravelApp.DTO.Citizen;
using SafeTravelApp.DTO.Destination;
using SafeTravelApp.DTO.User;

namespace SafeTravelApp.Services
{
    public interface ICitizenService
    {
        Task<CitizenDetailsReadOnlyDTO> SignUpUserAsync(CitizenSignUpDTO request);
        Task<List<CitizenReadOnlyDTO>> GetAllUsersCitizensAsync(int pageNumber, int pageSize);
        Task<List<CitizenReadOnlyDTO>> GetAllUsersCitizensFilteredAsync(CitizenDetailsFiltersDTO citizenDetaisFiltersDTO);

        Task<CitizenDetailsReadOnlyDTO?> GetCitizenByUsernameAsync(string username);
        Task<CitizenReadOnlyDTO?> GetCitizenByIdAsync(int id);
        Task<CitizenReadOnlyDTO?> GetCitizenByPhoneNumberAsync(string phoneNumber);

        Task<List<CitizenDestinationsReadOnlyDTO>> GetAllCitizenDestinationsFilteredAsync(Citizen citizen, CitizenDestinationFiltersDTO citizenDestinationFiltersDTO);

        Task<List<CitizenReadOnlyDTO>> GetAllDestinationCitizensFilteredAsync(Destination destination, CitizenRoleDetailsFiltersDTO citizenRoleDetailsFiltersDTO); 
        //Task<List<Recommendation>> GetAllCitizenRecommendationsFilteredAsync(int id, Recommendation recommendation);

    }
}

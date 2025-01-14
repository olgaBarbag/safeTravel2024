using SafeTravelApp.Core.Enums;
using SafeTravelApp.Data;

namespace SafeTravelApp.Repositories
{
    public interface ICitizenDestinationRepository
    {
        Task<List<Citizen>> GetCitizensByCitizenRoleAsync(CitizenRole citizenRole);                      //All citizens with specific role 
        Task<List<Citizen>> GetCitizensByCitizenRoleAsync(Destination destination, CitizenRole citizenRole);   //All citizens with specific role and specific destination
        Task<List<Destination>> GetDestinationsByCitizenRoleAsync(int citizenId, CitizenRole citizenRole); //All destinations of specific citizen with specific role
    }
}

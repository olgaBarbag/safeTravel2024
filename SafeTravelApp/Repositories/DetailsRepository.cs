using Microsoft.EntityFrameworkCore;
using SafeTravelApp.Core.Enums;
using SafeTravelApp.Core.Filters;
using SafeTravelApp.Data;

namespace SafeTravelApp.Repositories
{
    public class DetailsRepository : BaseRepository<UserDetails, int>, IDetailsRepository
    {
        public DetailsRepository(SafeTravelAppDbContext context) : base(context)
        {
        }

        public async Task<List<User>?> GetAllUsersWithDetailsAsync()
        {
            return await context.Users!
                .Include(u => u.Details)
                .ToListAsync();
        }

        public async Task<List<User>?> GetDetailsAllUsersAgents()
        {
            return await context.Users!
                .Include(u => u.Details)
                .Where(u => u.UserRole == UserRole.Agent)
                .ToListAsync();
        }

        public Task<List<AgentDetailsFiltersDTO>?> GetDetailsAllUsersAgentsFiltered(List<Func<UserDetails, bool>> predicates)
        {
            throw new NotImplementedException();
        }

        public async Task<List<User>?> GetDetailsAllUsersCitizens()
        {
            return await context.Users!
                .Include(u => u.Details)
                .Where(u => u.UserRole == UserRole.Citizen)
                .ToListAsync();
        }

        public Task<List<CitizenDetailsFiltersDTO>?> GetDetailsAllUsersCitizensFiltered(List<Func<UserDetails, bool>> predicates)
        {
            throw new NotImplementedException();
        }

        public Task<List<UserDetailsFiltersDTO>?> GetDetailsUsersFiltered(List<Func<UserDetails, bool>> predicates)
        {
            throw new NotImplementedException();
        }

        //-----------------------------------------------------------------------------------------------------------------------------------



        //-----------------------------------------------------------------------------------------------------------------------------------

        public async Task<UserDetails?> UpdateUserDetailsAsync(int id, UserDetails userDetails)
        {
            var existingDetails = await context.Details!.FirstOrDefaultAsync(u => u.UserId == userDetails.UserId);
            if (existingDetails is null) return null;
            if (existingDetails.UserId != id) return null;

            context.Details!.Attach(userDetails);
            context.Entry(userDetails).State = EntityState.Modified;
            return existingDetails;
        }


    }
}

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

        public async Task<List<User>?> GetDetailsAllUsersCitizens()
        {
            return await context.Users!
                .Include(u => u.Details)
                .Where(u => u.UserRole == UserRole.Citizen)
                .ToListAsync();
        }

        //-----------------------------------------------------------------------------------------------------------------------------------

        public async Task<List<AgentDetailsFiltersDTO>?> GetDetailsAllUsersAgentsFiltered(List<Func<UserDetails, bool>> predicates)
        {
            IQueryable<Agent> query = context.Agents!
                .Include(a => a.User)
                .Include(a => a.User.Details)
                .Where(a => a.User.UserRole == UserRole.Agent);

            if (predicates != null && predicates.Any())
            {
                query = query.Where(a => predicates.All(predicate => predicate(a.User.Details!)));
            }

            var result = await query.Select(a => new AgentDetailsFiltersDTO
            {
                AgentId = a.Id,
                CompanyName = a.CompanyName,
                VatNumber = a.VatNumber,
                Firstname = a.User.Firstname,
                Lastname = a.User.Lastname,
                PhoneNumber = a.User.Details!.PhoneNumber,
                Address = a.User.Details.Address,
                City = a.User.Details.City
            }).ToListAsync();

            return result;
        }

        public async Task<List<CitizenDetailsFiltersDTO>?> GetDetailsAllUsersCitizensFiltered(List<Func<UserDetails, bool>> predicates)
        {

            IQueryable<Citizen> query = context.Citizens!
                .Include(c => c.User)
                .Include(c => c.User.Details)
                .Where(c => c.User.UserRole == UserRole.Citizen);

            if (predicates != null && predicates.Any())
            {
                query = query.Where(c => predicates.All(predicate => predicate(c.User.Details!)));
            }

            var result = await query.Select(c => new CitizenDetailsFiltersDTO
            {
                CitizenId = c.Id,
                Firstname = c.User.Firstname,
                Lastname = c.User.Lastname,
                PhoneNumber = c.User.Details!.PhoneNumber,
                Address = c.User.Details.Address,
                City = c.User.Details.City
            }).ToListAsync();

            return result;
        }
                        
        public async Task<List<UserDetaisFiltersDTO>?> GetDetailsUsersFiltered(List<Func<UserDetails, bool>> predicates)
        {
            IQueryable<User> query = context.Users!.Include(u => u.Details);

            if (predicates != null && predicates.Any())
            {
                query = query.Where(u => predicates.All(predicate => predicate(u.Details!)));
            }

            var result = await query.Select(u => new UserDetaisFiltersDTO
            {
                Username = u.Username,
                Email = u.Email,
                Firstname = u.Firstname,
                Lastname = u.Lastname,
                UserRole = u.UserRole.ToString(),
                PhoneNumber = u.Details!.PhoneNumber,
                Address = u.Details.Address,
                City = u.Details.City
            }).ToListAsync();

            return result;

        }

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

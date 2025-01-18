using Microsoft.EntityFrameworkCore;
using SafeTravelApp.Core.Enums;
using SafeTravelApp.Data;
using SafeTravelApp.Models;
using System.Linq;

namespace SafeTravelApp.Repositories
{
    public class CitizenRepository : BaseRepository<Citizen, int>, ICitizenRepository
    {
        public CitizenRepository(SafeTravelAppDbContext context) : base(context)
        {

        }

        public async Task<List<Destination>> GetCitizenDestinationsFilteredAsync(int id, List<Func<Destination, bool>> predicates)
        {

            IQueryable<Destination> query = context.Citizens!
                .Where(c => c.Id == id)
                .SelectMany(c => c.Destinations!);

            if (predicates != null && predicates.Any())
            {
                query = query.Where(u => predicates.All(predicate => predicate(u)));
            }

            return await query.ToListAsync();
        }

        //--------------------------------------------------------------------------------------------------------------------------------------

        public async Task<List<Citizen>?> GetUserCitizensFilteredAsync(List<Func<User, bool>> predicates)
        {
            IQueryable<Citizen> query = context.Citizens!
                .Include(c => c.User)
                .Where(c => c.User.UserRole == UserRole.Citizen);


            if (predicates != null && predicates.Any())
            {
                query = query.Where(c => predicates.All(predicate => predicate(c.User)));
            }

            return await query.ToListAsync();
        }

        public async Task<List<Citizen>?> GetCitizensDestinationFilteredAsync(int id, List<Func<Citizen, bool>> predicates)
        {
            IQueryable<Citizen> query = context.Destinations!
                .Where(d => d.Id == id)
                .SelectMany(d => d.Citizens!);
              
            if (predicates != null && predicates.Any())
            {
                query = query.Where(u => predicates.All(predicate => predicate(u)));
            }

            return await query.ToListAsync();
        }

        //---------------------------------------------------------------------------------------------------------------------------------------

        public async Task<User?> GetUserCitizenByUsernameAsync(string username)
        {
            return await context.Users!
             .Include(u => u.Details)
             .Include(u => u.Citizen)
             .FirstOrDefaultAsync(u => u.UserRole == UserRole.Citizen && u.Username == username || u.Email == username);
        }

        public async Task<User?> GetUserCitizenByPhoneNumberAsync(string phoneNumber)
        {
            return await context.Users!
             .Include(u => u.Details)
             .Include(u => u.Citizen)
             .FirstOrDefaultAsync(u => u.UserRole == UserRole.Citizen && u.Details.PhoneNumber == phoneNumber);
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------

        public async Task<List<User>?> GetAllUsersCitizensAsync()
        {
            var usersWithCitizenRole = await context.Users!
                .Where(u => u.UserRole == UserRole.Citizen)
                .Include(u => u.Citizen)
                .ToListAsync();

            return usersWithCitizenRole;
        }

        public async Task<List<User>?> GetAllUsersCitizensPaginatedAsync(int pageNumber, int pageSize)
        {
            int skip = (pageNumber - 1) * pageSize;
            var usersWithAgentRole = await context.Users!
                .Where(u => u.UserRole == UserRole.Agent)
                .Include(u => u.Agent)
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();

            return usersWithAgentRole;
        }
                
        public async Task<PaginatedResult<User>> GetUsersCitizensPagedFilteredAsync(int pageNumber, int pageSize, List<Func<User, bool>> predicates)
        {
            var totalRecords = await context.Users!
                .Where(u => u.UserRole == UserRole.Citizen)
                .CountAsync();

            int skip = (pageNumber - 1) * pageSize;

            IQueryable<User> query = context.Users!
                .Where(u => u.UserRole == UserRole.Citizen)
                .Skip(skip)
                .Take(pageSize);

            if (predicates != null && predicates.Any())
            {
                query = query.Where(u => predicates.All(predicate => predicate(u)));
            }

            var usersCitizens = await query.ToListAsync();

            return new PaginatedResult<User>
            {
                Data = usersCitizens,
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }
                
    }
}

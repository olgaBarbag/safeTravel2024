using Microsoft.EntityFrameworkCore;
using SafeTravelApp.Core.Enums;
using SafeTravelApp.Data;
using SafeTravelApp.Models;
using System.Numerics;

namespace SafeTravelApp.Repositories
{
    public class AgentRepository : BaseRepository<Agent, int>, IAgentRepository
    {
        public AgentRepository(SafeTravelAppDbContext context) : base(context)
        {

        }

        public async Task<List<Language>> GetAgentLanguagesFilteredAsync(int id, List<Func<Language, bool>> predicates)
        {
            IQueryable<Language> query = context.Agents!
                .Where(t => t.Id == id)
                .SelectMany(t => t.Languages!);

            if(predicates != null && predicates.Any())
            {
                query = query.Where(u => predicates.All(predicate => predicate(u)));
            }

            return await query.ToListAsync();
        }

        public async Task<List<Certification>> GetAgentCertificationsFilteredAsync(int id)
        {
            IQueryable<Certification> query = context.Agents!
                .Where(t => t.Id == id)
                .SelectMany(t => t.Certifications!);
           
            return await query.ToListAsync();
        }

        public async Task<List<Destination>> GetAgentDestinationsFilteredAsync(int id, List<Func<Destination, bool>> predicates)
        {
            IQueryable<Destination> query = context.Agents!
                .Where(t => t.Id == id)
                .SelectMany(t => t.Destinations!);

            if (predicates != null && predicates.Any())
            {
                query = query.Where(u => predicates.All(predicate => predicate(u)));
            }

            return await query.ToListAsync();
        }

        //--------------------------------------------------------------------------------------------------------------------------------

        public async Task<List<Agent>> GetDestinationAgentsFilteredAsync(int destId, List<Func<User, bool>> predicates)
        {
            IQueryable<Agent> query = context.Destinations!
                .Where(t => t.Id == destId)
                .SelectMany(a => a.Agents!);

            if (predicates != null && predicates.Any())
            {
                query = query.Where(u => predicates.All(predicate => predicate(u.User)));
            }

            return await query.ToListAsync();
        }

        public async Task<List<Agent>> GetLanguageAgentsFilteredAsync(List<Func<Language, bool>> predicates)
        {
            IQueryable<Agent> query = context.Agents!
                .Include(a => a.Languages);

            if (predicates != null && predicates.Any())
            {
                query = query.Where(a => a.Languages!.Any(l => predicates.All(predicate => predicate(l))));
            }

            return await query.ToListAsync();
        }

        public async Task<List<Agent>> GetUserAgentsFilteredAsync(List<Func<User, bool>> predicates)
        {
            IQueryable<Agent> query = context.Agents!
                .Include(a => a.User)
                .Where(a => a.User.UserRole == UserRole.Agent);
                

            if (predicates != null && predicates.Any())
            {
                query = query.Where(a => predicates.All(predicate => predicate(a.User)));
            }

            return await query.ToListAsync();
        }
        
        //--------------------------------------------------------------------------------------------------------------------------------
               
        public async Task<User?> GetUserAgentByUsernameAsync(string username)
        {
            return await context.Users!
             .Include(u => u.Details) 
             .Include(u => u.Agent)  
             .FirstOrDefaultAsync(u => u.UserRole == UserRole.Agent && u.Username == username || u.Email == username);
        }

        public async Task<User?> GetUserAgentByPhoneNumberAsync(string phoneNumber)
        {
            return await context.Users!
             .Include(u => u.Details)
             .Include(u => u.Agent)
             .FirstOrDefaultAsync(u => u.UserRole == UserRole.Agent && u.Details!.PhoneNumber == phoneNumber);
        }

        //--------------------------------------------------------------------------------------------------------------------------------

        public async Task<List<User>?> GetAllUsersAgentsAsync()
        {
            var usersWithAgentsRole = await context.Users!
                .Where(u => u.UserRole == UserRole.Agent)
                .Include(u => u.Agent)    
                .ToListAsync();

            return usersWithAgentsRole;
        }

        public async Task<List<User>?> GetAllUsersAgentsPaginatedAsync(int pageNumber, int pageSize)
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

        public async Task<PaginatedResult<User>> GetUsersAgentsPagedFilteredAsync(int pageNumber, int pageSize, List<Func<User, bool>> predicates)
        {
            var totalRecords = await context.Users!
                .Where(u => u.UserRole == UserRole.Agent)
                .CountAsync();

            int skip = (pageNumber - 1) * pageSize;

            IQueryable<User> query = context.Users!
                .Where(u => u.UserRole == UserRole.Agent)
                .Skip(skip)
                .Take(pageSize);

            if (predicates != null && predicates.Any())
            {
                query = query.Where(u => predicates.All(predicate => predicate(u)));
            }

            var usersAgents = await query.ToListAsync();

            return new PaginatedResult<User>
            {
                Data = usersAgents,
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

    }
}

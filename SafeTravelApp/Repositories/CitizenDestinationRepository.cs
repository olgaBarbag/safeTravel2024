using Microsoft.EntityFrameworkCore;
using SafeTravelApp.Core.Enums;
using SafeTravelApp.Data;
using SafeTravelApp.Models.Keys;

namespace SafeTravelApp.Repositories
{
    public class CitizenDestinationRepository : BaseRepository<CitizenDestination, CitizenDestinationKey>, ICitizenDestinationRepository
    {
        public CitizenDestinationRepository(SafeTravelAppDbContext context) : base(context)
        {

        }

        public async Task<List<Citizen>> GetCitizensByCitizenRoleAsync(CitizenRole citizenRole)
        {
            IQueryable<Citizen> query = context.Citizens!
             .Include(c => c.CitizenDestinations)
             .Where(c => c.CitizenDestinations.Any(cd => cd.CitizenRole == citizenRole));

            return await query.ToListAsync();
        }

        public async Task<List<Citizen>> GetCitizensByCitizenRoleAsync(Destination destination, CitizenRole citizenRole)
        {
            IQueryable<Citizen> query = context.Citizens!
             .Include(c => c.CitizenDestinations)
             .Where(c => c.CitizenDestinations!.Any(cd => (cd.CitizenRole == citizenRole && cd.Destination == destination)));

            return await query.ToListAsync();
        }

        public async Task<List<Destination>> GetDestinationsByCitizenRoleAsync(int citizenId, CitizenRole citizenRole)
        {
            IQueryable<Destination> query = context.Destinations!
             .Include(d => d.CitizenDestinations)
             .Where(d => d.CitizenDestinations!.Any(cd => (cd.CitizenRole == citizenRole && cd.CitizenId == citizenId)));

            return await query.ToListAsync();
        }
    }
}

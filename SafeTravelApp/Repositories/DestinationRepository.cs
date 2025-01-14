using Microsoft.EntityFrameworkCore;
using SafeTravelApp.Core.Enums;
using SafeTravelApp.Data;

namespace SafeTravelApp.Repositories
{
    public class DestinationRepository : BaseRepository<Destination, int>, IDestinationRepository
    {
        public DestinationRepository(SafeTravelAppDbContext context) : base(context)
        {

        }

        public async Task<List<Agent>> GetAgentsByDestinationAsync(int destinationId)
        {
            IQueryable<Agent> query = context.Destinations!
                .Where(d => d.Id == destinationId)
                .SelectMany(d => d.Agents!);

            return await query.ToListAsync();

        }

        public async Task<List<Citizen>> GetCitizensByDestinationAsync(int destinationId)
        {
            IQueryable<Citizen> query = context.Destinations!
                .Where(d => d.Id == destinationId)
                .SelectMany(d => d.Citizens!);

            return await query.ToListAsync();
        }
        
        public async Task<List<Recommendation>> GetRecommendationsByDestinationAsync(int destinationId)
        {
            IQueryable<Recommendation> query = context.Destinations!
                .Where(d => d.Id == destinationId)
                .SelectMany(d => d.Recommendations!);

            return await query.ToListAsync();
        }

        public async Task<List<Destination>> GetDestinationsByTypeAsync(DestinationType type)
        {
            return await context.Destinations!
                 .Where(d => d.Type == type)
                 .ToListAsync();
        }

        
    }
}

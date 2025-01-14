using Microsoft.EntityFrameworkCore;
using SafeTravelApp.Core.Enums;
using SafeTravelApp.Data;

namespace SafeTravelApp.Repositories
{
    public class CategoryRepository : BaseRepository<Category, int>, ICategoryRepository
    {
        public CategoryRepository(SafeTravelAppDbContext context) : base(context)
        {

        }

        public async Task<List<Recommendation>> GetRecommendationsByDestinationAndCategoryAsync(int destinationId, DestinationCategory destinationCategory)
        {
            IQueryable<Recommendation> query = context.Destinations!
                .SelectMany(d => d.Categories!)
                .Where(c => c.DestinationCategory == destinationCategory)
                .SelectMany(c => c.Recommendations!);

            return await query.ToListAsync();
        }

        public async Task<List<Agent>> GetAgentsByDestinationAndCategoryAsync(int destinationId, DestinationCategory destinationCategory)
        {
            IQueryable<Agent> query = context.Agents!
             .Where(a => a.Destinations!
                .Any(d => d.Id == destinationId && d.Categories!.Any(c => c.DestinationCategory == destinationCategory)));

            return await query.ToListAsync();
        }

        public async Task<List<Citizen>> GetCitizensByDestinationAndCategoryAsync(int destinationId, DestinationCategory destinationCategory)
        {
            IQueryable<Citizen> query = context.Citizens!
             .Where(a => a.Destinations!
                .Any(d => d.Id == destinationId && d.Categories!.Any(c => c.DestinationCategory == destinationCategory)));

            return await query.ToListAsync();
        }

        public async Task<int> CountAgentsByDestinationAndCategoryAsync(int destId, DestinationCategory destCategory)
        {
            var agents = await GetAgentsByDestinationAndCategoryAsync(destId, destCategory);
            return agents.Count();
        }

        public async Task<int> CountCitizensByDestinationAndCategoryAsync(int destId, DestinationCategory destCategory)
        {
            var count = await context.Citizens!
        .Where(a => a.Destinations!
            .Any(d => d.Id == destId && d.Categories!.Any(c => c.DestinationCategory == destCategory)))
        .CountAsync();

            return count;

        }

    }
}

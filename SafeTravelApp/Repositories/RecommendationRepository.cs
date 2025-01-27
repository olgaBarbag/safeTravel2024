using Microsoft.EntityFrameworkCore;
using SafeTravelApp.Core.Enums;
using SafeTravelApp.Data;
using System.Linq;

namespace SafeTravelApp.Repositories
{
    public class RecommendationRepository : BaseRepository<Recommendation, int>, IRecommendationRepository
    {
        public RecommendationRepository(SafeTravelAppDbContext context) : base(context)
        {

        }

        public async Task<List<Recommendation>> GetRecommendationsByContributorId(int contributorId)
        {
            return await context.Recommendations!
                .Where(r => r.ContributorId == contributorId)
                .ToListAsync();
        }

        public async Task<List<Recommendation>> GetRecommendationsByDestinationAndContributorType(int destId, ContributorRole role)
        {
            return await context.Recommendations!
                .Where(r => (r.DestinationId == destId && r.ContributorRole == role))
                .ToListAsync();
           
        }

        public async Task<List<Recommendation>> GetRecommendationsByDestinationFiltered(List<Func<Destination, bool>> predicates)
        {
            IQueryable<Recommendation> query = context.Destinations!
                .SelectMany(d => d.Recommendations!);



            if (predicates != null && predicates.Any())
            {
                query = query.Where(r => predicates.All(predicate => predicate(r.Destination)));
            }

            return await query.ToListAsync();
        }
                
        public async Task<ContributorRole> GetContributorRole(User user, Destination destination)
        {
            switch (user.UserRole)
            {
                case UserRole.Agent:
                    return ContributorRole.Agent;

                case UserRole.Citizen:

                    bool isLocal = destination.Country == user.Details.Country && destination.City == user.Details.City;
                    return isLocal ? ContributorRole.Local : ContributorRole.Explorer;


                default:
                    return ContributorRole.Explorer;
            }
        }

        public async Task<Recommendation?> UpdateRecommendationAsync(int userId, Recommendation recommendation)
        {
            var existingRecommendation = await context.Recommendations!.FirstOrDefaultAsync(u => u.Id == recommendation.Id);
            if (existingRecommendation is null) return null;
            if (existingRecommendation.ContributorId != userId) return null;  //ωστε να μην παει να τροποποιησει καποιος αλλον χρηστη

            context.Recommendations!.Attach(recommendation);
            context.Entry(recommendation).State = EntityState.Modified;
            return existingRecommendation;
        }


    }
}

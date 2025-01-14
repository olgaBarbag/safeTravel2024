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

        public async Task SetContributorRoleAsync(int recommendationId, int contributorId)
        {
            
            var recommendation = await context.Recommendations!
                .FirstOrDefaultAsync(r => r.Id == recommendationId);

            if (recommendation == null)
            {
                throw new Exception("Recommendation not found.");
            }
                      
            var user = await context.Users!
                .FirstOrDefaultAsync(u => u.Id == contributorId);

            if (user == null)
            {
                throw new Exception("User not found.");
            }
                        
            if (user.UserRole == UserRole.Agent)
            {
                recommendation.ContributorRole = ContributorRole.Agent;
            }
            else if (user.UserRole == UserRole.Citizen)
            {
                var userDetails = await context.Details!
                    .FirstOrDefaultAsync(ud => ud.UserId == user.Id);

                if (userDetails != null && recommendation.Destination != null)
                {
                    
                    if (userDetails.Country == recommendation.Destination.Country &&
                        userDetails.City == recommendation.Destination.City)
                    {
                        
                        recommendation.ContributorRole = ContributorRole.Local;
                    }
                    else
                    {
                        
                        recommendation.ContributorRole = ContributorRole.Visitor;
                    }
                }
            }
            
            await context.SaveChangesAsync();
        }
    }
}

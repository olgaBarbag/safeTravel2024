using SafeTravelApp.Data;

namespace SafeTravelApp.Repositories
{
    public class LanguageRepository : BaseRepository<Language, int>, ILanguageRepository
    {
        public LanguageRepository(SafeTravelAppDbContext context) : base(context)
        {

        }

        public Task<List<Agent>> GetLanguageAgentsAsyncFiltered(int id, List<Func<Agent, bool>> predicates)
        {
            throw new NotImplementedException();
        }
    }
}

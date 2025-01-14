using SafeTravelApp.Data;

namespace SafeTravelApp.Repositories
{
    public interface ILanguageRepository
    {
        Task<List<Agent>> GetLanguageAgentsAsyncFiltered(int id, List<Func<Agent, bool>> predicates);

    }
}

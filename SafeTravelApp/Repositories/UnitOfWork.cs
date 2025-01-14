using SafeTravelApp.Data;

namespace SafeTravelApp.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SafeTravelAppDbContext _context;
        public UnitOfWork(SafeTravelAppDbContext context)
        {
            _context = context;
        }

        public UserRepository UserRepository => new(_context);

        public AgentRepository AgentRepository => new(_context);

        public CitizenRepository CitizenRepository => new(_context);    

        public DestinationRepository DestinationRepository => new(_context);

        public CitizenDestinationRepository CitizenDestinationRepository => new(_context);

        public CategoryRepository CategoryRepository => new(_context);

        public RecommendationRepository RecommendationRepository => new(_context);

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}

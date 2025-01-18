using SafeTravelApp.Data;

namespace SafeTravelApp.Repositories
{
    public class CertificationRepository : BaseRepository<Certification, int>, ICertificationRepository
    {
        public CertificationRepository(SafeTravelAppDbContext context) : base(context)
        {

        }
    }
}

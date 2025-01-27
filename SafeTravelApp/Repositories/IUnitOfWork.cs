namespace SafeTravelApp.Repositories
{
    public interface IUnitOfWork
    {
        UserRepository UserRepository { get; }
        DetailsRepository DetailsRepository { get; }
        AgentRepository AgentRepository { get; }
        LanguageRepository LanguageRepository { get; }
        CertificationRepository CertificationRepository { get; }
        CitizenRepository CitizenRepository { get; }
        DestinationRepository DestinationRepository { get; }
        CitizenDestinationRepository CitizenDestinationRepository { get; }
        CategoryRepository CategoryRepository {  get; }
        RecommendationRepository RecommendationRepository { get; }

        Task<bool> SaveAsync();
    }
}

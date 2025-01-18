using AutoMapper;
using SafeTravelApp.Repositories;

namespace SafeTravelApp.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public ApplicationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public UserService UserService => new(_unitOfWork, _mapper);

        public AgentService AgentService => new(_unitOfWork, _mapper);

        public CitizenService CitizenService => new(_unitOfWork, _mapper);

        public DestinationService DestinationService => new(_unitOfWork, _mapper);

        public RecommendationService RecommendationService => new(_unitOfWork, _mapper);
    }
}

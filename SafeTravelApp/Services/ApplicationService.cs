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

        public UserService UserService => throw new NotImplementedException();

        public AgentService AgentService => throw new NotImplementedException();

        public CitizenService CitizenService => throw new NotImplementedException();
    }
}

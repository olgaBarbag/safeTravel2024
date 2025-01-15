using AutoMapper;
using SafeTravelApp.Core.Filters;
using SafeTravelApp.Data;
using SafeTravelApp.DTO.Agent;
using SafeTravelApp.DTO.User;
using SafeTravelApp.Repositories;
using Serilog;

namespace SafeTravelApp.Services
{
    public class AgentService : IAgentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<AgentService> _logger;

        public AgentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = new LoggerFactory().AddSerilog().CreateLogger<AgentService>();
        }

        public Task<AgentReadOnlyDTO?> GetAgentByUsernameAsync(string username)
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> GetAllUsersAgentsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> GetAllUsersAgentsFilteredAsync(UserDetaisFiltersDTO userDetaisFiltersDTO)
        {
            throw new NotImplementedException();
        }

        public Task<UserReadOnlyDTO> SignUpUserAsync(AgentSignUpDTO request)
        {
            throw new NotImplementedException();
        }
    }
}

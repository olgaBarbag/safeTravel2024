using AutoMapper;
using SafeTravelApp.Core.Filters;
using SafeTravelApp.Data;
using SafeTravelApp.DTO.Agent;
using SafeTravelApp.DTO.Citizen;
using SafeTravelApp.DTO.User;
using SafeTravelApp.Repositories;
using Serilog;

namespace SafeTravelApp.Services
{
    public class CitizenService : ICitizenService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CitizenService> _logger;

        public CitizenService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = new LoggerFactory().AddSerilog().CreateLogger<CitizenService>();
        }

        public Task<AgentReadOnlyDTO?> GetCitizenByUsernameAsync(string username)
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> GetAllUsersCitizensAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> GetAllUsersCitizensFilteredAsync(UserDetaisFiltersDTO userDetaisFiltersDTO)
        {
            throw new NotImplementedException();
        }

        public Task<UserReadOnlyDTO> SignUpUserAsync(CitizenSignUpDTO request)
        {
            throw new NotImplementedException();
        }
    }
}

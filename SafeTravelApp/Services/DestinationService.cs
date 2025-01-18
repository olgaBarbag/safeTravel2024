using AutoMapper;
using SafeTravelApp.Repositories;
using Serilog;

namespace SafeTravelApp.Services
{
    public class DestinationService : IDestinationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<DestinationService> _logger;

        public DestinationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = new LoggerFactory().AddSerilog().CreateLogger<DestinationService>();
        }
    }
}

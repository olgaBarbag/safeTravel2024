using AutoMapper;
using SafeTravelApp.Core.Filters;
using SafeTravelApp.Data;
using SafeTravelApp.DTO.Agent;
using SafeTravelApp.DTO.Citizen;
using SafeTravelApp.DTO.User;
using SafeTravelApp.Exceptions;
using SafeTravelApp.Repositories;
using SafeTravelApp.Security;
using Serilog;
using System;

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


        public async Task<AgentDetailsReadOnlyDTO> SignUpUserAsync(AgentSignUpDTO request)
        {
            Agent agent;
            User user;
            
            try
            {
                user = _mapper.Map<User>(request);
                User? existingUser = await _unitOfWork.UserRepository.GetByUsernameAsync(user.Username);

                if (existingUser != null)
                {
                    throw new EntityAlreadyExistsException("User", "User: " + existingUser.Username + " already exists");
                }

                user.Password = EncryptionUtil.Encrypt(user.Password);
                await _unitOfWork.UserRepository.AddAsync(user);

                //if (user.Details == null)
                //{
                //    user.Details = new UserDetails
                //    {
                //        PhoneNumber = request.PhoneNumber!,
                //        Country = request.Country!,
                //        City = request.City!,
                //        Address = request.Address,
                //        PostalCode = request.PostalCode,
                //        UserId = user.Id 
                //    };
                //    await _unitOfWork.DetailsRepository.AddAsync(user.Details);
                //}


                agent = _mapper.Map<Agent>(request);

                await _unitOfWork.AgentRepository.AddAsync(agent);
                user.Agent = agent;

                await _unitOfWork.SaveAsync();
                _logger.LogInformation("{Message}", "Agent: " + agent + " signed up successfully.");
                return _mapper.Map<AgentDetailsReadOnlyDTO>(user); ;
            }
            catch (Exception ex)
            {
                _logger.LogError("{Message}{Exception}", ex.Message, ex.StackTrace);
                throw;
            }
        }

        public async Task<List<AgentReadOnlyDTO>> GetAllUsersAgentsAsync(int pageNumber, int pageSize)
        {
            List<AgentReadOnlyDTO>? agentReadOnlyDTOs = new();

            try
            {
                List<User>? usersAgents = await _unitOfWork.AgentRepository.GetAllUsersAgentsPaginatedAsync(pageNumber, pageSize);

                if (usersAgents == null || !usersAgents.Any())
                {
                    _logger.LogInformation("{Message}","No agents found.");
                    return null; // Or return an empty list
                }

                agentReadOnlyDTOs = _mapper.Map<List<AgentReadOnlyDTO>>(usersAgents);

                _logger.LogInformation("{Message}", "All sorting agents returned");
            }
            catch (Exception ex)
            {
                _logger.LogError("{Message}{Exception}", ex.Message, ex.StackTrace);

            }
            return agentReadOnlyDTOs;
        }

        public async Task<List<AgentReadOnlyDTO>> GetAllUsersAgentsFilteredAsync(AgentDetailsFiltersDTO agentDetailsFiltersDTO)
        {
            List<AgentReadOnlyDTO>? agentReadOnlyDTOs = new();
            
            try
            {
                List<Func<User, bool>> predicates = GetUserPredicates(agentDetailsFiltersDTO);
                List<Agent>? agentUsers = await _unitOfWork.AgentRepository.GetUserAgentsFilteredAsync(predicates);

                if (agentUsers == null || !agentUsers.Any())
                {
                    _logger.LogInformation("{Message}", "No agents found with those creteria.");
                    return null; // Or return an empty list
                }

                agentReadOnlyDTOs = _mapper.Map<List<AgentReadOnlyDTO>>(agentUsers);

                _logger.LogInformation("{Message}", "All sorting agents returned");
            }
            catch (Exception ex)
            {

                _logger.LogError("{Message}{Exception}", ex.Message, ex.StackTrace);
            }
            return agentReadOnlyDTOs;
        }

        public async Task<AgentDetailsReadOnlyDTO?> GetAgentByIdAsync(int id)
        {
            AgentDetailsReadOnlyDTO? agentDetailsReadOnlyDTO = null;

            try
            {
                Agent? agent = await _unitOfWork.AgentRepository.GetByIdAsync(id);

                if (agent == null)
                {
                    _logger.LogInformation("{Message}", "Agent with id: " + id + "was not found");
                    return null;
                }

                agentDetailsReadOnlyDTO = _mapper.Map<AgentDetailsReadOnlyDTO>(agent);
                _logger.LogInformation("{Message}", "Agent with id: " + id + " was found");
            }
            catch (Exception ex)
            {

                _logger.LogError("{Message}{Excpetion}", ex.Message, ex.StackTrace);
            }

            return agentDetailsReadOnlyDTO;
        }

        public async Task<AgentDetailsReadOnlyDTO?> GetAgentByPhoneNumberAsync(string phoneNumber)
        {
            AgentDetailsReadOnlyDTO? agentReadOnlyDTO = null;

            try
            {
                User? user = await _unitOfWork.AgentRepository.GetUserAgentByPhoneNumberAsync(phoneNumber);

                if (user == null)
                {
                    _logger.LogInformation("{Message}", "Agent with phoneNumber: " + phoneNumber + "was not found");
                    return null;
                }

                agentReadOnlyDTO = _mapper.Map<AgentDetailsReadOnlyDTO>(user);
                _logger.LogInformation("{Message}", "Agent with phoneNumber: " + phoneNumber + " was found");
            }
            catch (Exception ex)
            {

                _logger.LogError("{Message}{Excpetion}", ex.Message, ex.StackTrace);
            }

            return agentReadOnlyDTO;
        }

        public async Task<AgentDetailsReadOnlyDTO?> GetAgentByUsernameAsync(string username)
        {
            AgentDetailsReadOnlyDTO? agentDetailsReadOnlyDTO = null;

            try
            {
                User? user = await _unitOfWork.AgentRepository.GetUserAgentByUsernameAsync(username);

                if (user == null)
                {
                    _logger.LogInformation("{Message}", "Agent: " + username + "was not found");
                    return null;
                }

                agentDetailsReadOnlyDTO = _mapper.Map<AgentDetailsReadOnlyDTO>(user);
                _logger.LogInformation("{Message}", "Agent: " + username + " was found");
            }
            catch (Exception ex)
            {

                _logger.LogError("{Message}{Excpetion}", ex.Message, ex.StackTrace);
            }

            return agentDetailsReadOnlyDTO;
        }

        public async Task<List<Destination>> GetAllAgentDestinationsFilteredAsync(Agent agent, DestinationFiltersDTO destinationFiltersDTO)
        {
            List<Destination> destinations = new();
            try
            {
                Agent? existingAgent = await _unitOfWork.AgentRepository.GetByIdAsync(agent.Id);
                if (existingAgent == null)
                {
                    _logger.LogInformation("{Message}", "Agent with id: " + agent.Id + " does not exist.");
                }
                List<Func<Destination, bool>> predicates = GetDestinationPredicates(destinationFiltersDTO);
                destinations = await _unitOfWork.AgentRepository.GetAgentDestinationsFilteredAsync(existingAgent!.Id, predicates);
                if (destinations == null || !destinations.Any())
                {
                    _logger.LogInformation("{Message}", "No destinations found who fullfills the criteria for that agent.");
                    return null; // Or return an empty list
                }
                _logger.LogInformation("{Message}", "Destinations who fullfills the criteria for that agent retrieved");
            }
            catch (Exception ex)
            {

                _logger.LogError("{Message}{Excpetion}", ex.Message, ex.StackTrace);
            }
            return destinations;
        }

        public async Task<List<AgentReadOnlyDTO>> GetAllDestinationAgentsFilteredAsync(Destination destination, AgentDetailsFiltersDTO agentDetailsFiltersDTO)
        {
            List<AgentReadOnlyDTO> agentReadOnlyDTOs = new();
            try
            {
                Destination? existingDestination = await _unitOfWork.DestinationRepository.GetByIdAsync(destination.Id);
                if (existingDestination == null)
                {
                    _logger.LogInformation("{Message}", "Destination with id: " + destination.Id + " does not exist.");
                    return null;
                }

                List<Func<User, bool>> predicates = GetUserPredicates(agentDetailsFiltersDTO);

                List<Agent>? agents = await _unitOfWork.AgentRepository.GetDestinationAgentsFilteredAsync(existingDestination!.Id, predicates);
                if (agents == null || !agents.Any())
                {
                    _logger.LogInformation("{Message}", "No agents found who fullfills the criteria for that destination.");
                    return null; // Or return an empty list
                }

                agentReadOnlyDTOs = _mapper.Map<List<AgentReadOnlyDTO>>(agents);

                _logger.LogInformation("{Message}", "All agents who fullfills the criteria for that destination returned");

               
            }
            catch (Exception ex)
            {

                _logger.LogError("{Message}{Excpetion}", ex.Message, ex.StackTrace);
            }
            return agentReadOnlyDTOs;
        }



        public async Task<List<Language>> GetAllAgentLanguagesFilteredAsync(Agent agent, LanguageFiltersDTO languageFiltersDTO)
        {
            List<Language> languages = new();
            try
            {
                Agent? existingAgent = await _unitOfWork.AgentRepository.GetByIdAsync(agent.Id);
                if (existingAgent == null)
                {
                    _logger.LogInformation("{Message}", "Agent with id: " + agent.Id + " does not exist.");
                }
                List<Func<Language, bool>> predicates = GetLanguagePredicates(languageFiltersDTO);
                languages = await _unitOfWork.AgentRepository.GetAgentLanguagesFilteredAsync(existingAgent!.Id, predicates);

                _logger.LogInformation("{Message}", "Agent languages retrieved");
            }
            catch (Exception ex)
            {

                _logger.LogError("{Message}{Excpetion}", ex.Message, ex.StackTrace);
            }
            return languages;
        }

        public async Task<List<Certification>> GetAllAgentCertificationsAsync(Agent agent)
        {
            List<Certification> certifications = new();

            try
            {
                Agent? existingAgent = await _unitOfWork.AgentRepository.GetByIdAsync(agent.Id);
                if (existingAgent == null)
                {
                    _logger.LogInformation("{Message}", "Agent with id: " + agent.Id + " does not exist.");
                }
                certifications = await _unitOfWork.AgentRepository.GetAgentCertificationsFilteredAsync(existingAgent!.Id);
                _logger.LogInformation("{Message}", "Agent certifications retrieved");

            }
            catch (Exception ex)
            {

                _logger.LogError("{Message}{Excpetion}", ex.Message, ex.StackTrace);
            }

            return certifications;
        }

        //public Task<List<Recommendation>> GetAllAgentRecommendationsFilteredAsync(int id, Recommendation recommendation)
        //{
        //    throw new NotImplementedException();
        //}
//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private List<Func<User, bool>> GetUserPredicates(AgentDetailsFiltersDTO agentDetailsFiltersDTO)
        {
            List<Func<User, bool>> predicates = new();

            if (!string.IsNullOrEmpty(agentDetailsFiltersDTO.Username))
            {
                predicates.Add(u => u.Username == agentDetailsFiltersDTO.Username);
            }
            if (!string.IsNullOrEmpty(agentDetailsFiltersDTO.Email))
            {
                predicates.Add(u => u.Email == agentDetailsFiltersDTO.Email);
            }
            if (!string.IsNullOrEmpty(agentDetailsFiltersDTO.Firstname))
            {
                predicates.Add(u => u.Firstname == agentDetailsFiltersDTO.Firstname);
            }
            if (!string.IsNullOrEmpty(agentDetailsFiltersDTO.Lastname))
            {
                predicates.Add(u => u.Lastname == agentDetailsFiltersDTO.Lastname);
            }
            if (!string.IsNullOrEmpty(agentDetailsFiltersDTO.PhoneNumber))
            {
                predicates.Add(u => u.Details!.PhoneNumber == agentDetailsFiltersDTO.PhoneNumber);
            }
            if (!string.IsNullOrEmpty(agentDetailsFiltersDTO.Country))
            {
                predicates.Add(u => u.Details!.Country == agentDetailsFiltersDTO.Country);
            }
            if (!string.IsNullOrEmpty(agentDetailsFiltersDTO.City))
            {
                predicates.Add(u => u.Details!.City == agentDetailsFiltersDTO.City);
            }
            if (!string.IsNullOrEmpty(agentDetailsFiltersDTO.CompanyName))
            {
                predicates.Add(u => u.Agent!.CompanyName == agentDetailsFiltersDTO.CompanyName);
            }
            if (!string.IsNullOrEmpty(agentDetailsFiltersDTO.VatNumber))
            {
                predicates.Add(u => u.Agent!.VatNumber == agentDetailsFiltersDTO.VatNumber);
            }

            return predicates;
        }
        private List<Func<Language, bool>> GetLanguagePredicates(LanguageFiltersDTO languageFiltersDTO)
        {
            List<Func<Language, bool>> predicates = new();

            if (!string.IsNullOrEmpty(languageFiltersDTO.LanguageName))
            {
                predicates.Add(l => l.LanguageName == languageFiltersDTO.LanguageName);
            }
            if (!string.IsNullOrEmpty(languageFiltersDTO.LanguageName))
            {
                predicates.Add(l => l.Level == languageFiltersDTO.Level);
            }

            return predicates;
        }

        private List<Func<Destination, bool>> GetDestinationPredicates(DestinationFiltersDTO destinationFiltersDTO)
        {
            List<Func<Destination, bool>> predicates = new();

            if (!string.IsNullOrEmpty(destinationFiltersDTO.Country))
            {
                predicates.Add(d => d.Country == destinationFiltersDTO.Country);
            }
            if (!string.IsNullOrEmpty(destinationFiltersDTO.City))
            {
                predicates.Add(d => d.City == destinationFiltersDTO.City);
            }
            if (!string.IsNullOrEmpty(destinationFiltersDTO.Region))
            {
                predicates.Add(d => d.Region == destinationFiltersDTO.Region);
            }
            if (!string.IsNullOrEmpty(destinationFiltersDTO.Type))
            {
                predicates.Add(d => d.Type.ToString() == destinationFiltersDTO.Type);
            }
            return predicates;
        }
    }
}

using AutoMapper;
using SafeTravelApp.Core.Enums;
using SafeTravelApp.Core.Filters;
using SafeTravelApp.Data;
using SafeTravelApp.DTO.Agent;
using SafeTravelApp.DTO.Citizen;
using SafeTravelApp.DTO.Destination;
using SafeTravelApp.DTO.User;
using SafeTravelApp.Exceptions;
using SafeTravelApp.Repositories;
using SafeTravelApp.Security;
using Serilog;
using System.Linq;

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

        public async Task<CitizenDetailsReadOnlyDTO> SignUpUserAsync(CitizenSignUpDTO request)
        {
            Citizen citizen;
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

                citizen = _mapper.Map<Citizen>(request);

                await _unitOfWork.CitizenRepository.AddAsync(citizen);
                user.Citizen = citizen;
                
                await _unitOfWork.SaveAsync();
                _logger.LogInformation("{Message}", "Citizen: " + citizen + " signed up successfully.");
                return _mapper.Map<CitizenDetailsReadOnlyDTO>(user); ;
            }
            catch (Exception ex)
            {
                _logger.LogError("{Message}{Exception}", ex.Message, ex.StackTrace);
                throw;
            }
        }

        public async Task<List<CitizenReadOnlyDTO>?> GetAllUsersCitizensAsync(int pageNumber, int pageSize)
        {
            List<CitizenReadOnlyDTO>? citizenReadOnlyDTOs= new();
                        
            try
            {
                List<User>? usersCitizens = await _unitOfWork.CitizenRepository.GetAllUsersCitizensPaginatedAsync(pageNumber, pageSize);

                if (usersCitizens == null || !usersCitizens.Any())
                {
                    _logger.LogInformation("{Message}", "No citizens found.");
                    return null; // Or return an empty list
                }

                citizenReadOnlyDTOs = _mapper.Map<List<CitizenReadOnlyDTO>>(usersCitizens);
                
                _logger.LogInformation("{Message}", "All sorting citizens returned");
            }
            catch (Exception ex)
            {
                _logger.LogError("{Message}{Exception}", ex.Message, ex.StackTrace);

            }
            return citizenReadOnlyDTOs;
        }

        public async Task<List<CitizenReadOnlyDTO>> GetAllUsersCitizensFilteredAsync(CitizenDetailsFiltersDTO citizenDetaisFiltersDTO)
        {
            List<CitizenReadOnlyDTO>? citizenReadOnlyDTOs = new();
                       
            try
            {
                List<Func<User, bool>> predicates = GetUserPredicates(citizenDetaisFiltersDTO);
                List<Citizen>? citizenUsers = await _unitOfWork.CitizenRepository.GetUserCitizensFilteredAsync(predicates);

                if (citizenUsers == null || !citizenUsers.Any())
                {
                    _logger.LogInformation("{Message}", "No citizens found with those creteria.");
                    return null; // Or return an empty list
                }

                citizenReadOnlyDTOs = _mapper.Map<List<CitizenReadOnlyDTO>>(citizenUsers);

                _logger.LogInformation("{Message}", "All sorting citizens returned");
            }
            catch (Exception ex)
            {

                _logger.LogError("{Message}{Exception}", ex.Message, ex.StackTrace);
            }
            return citizenReadOnlyDTOs;
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public async Task<CitizenReadOnlyDTO?> GetCitizenByIdAsync(int id)
        {
            CitizenReadOnlyDTO? citizenReadOnlyDTO = null;

            try
            {
                Citizen? citizen = await _unitOfWork.CitizenRepository.GetByIdAsync(id);

                if (citizen == null)
                {
                    _logger.LogInformation("{Message}", "Citizen with id: " + id + "was not found");
                    return null;
                }

                citizenReadOnlyDTO = _mapper.Map<CitizenReadOnlyDTO>(citizen);
                _logger.LogInformation("{Message}", "Citizen with id: " + id + " was found");
            }
            catch (Exception ex)
            {

                _logger.LogError("{Message}{Excpetion}", ex.Message, ex.StackTrace);
            }

            return citizenReadOnlyDTO;
        }

        public async Task<CitizenReadOnlyDTO?> GetCitizenByPhoneNumberAsync(string phoneNumber)
        {
            CitizenReadOnlyDTO? citizenReadOnlyDTO = null;

            try
            {
                User? userCitizen = await _unitOfWork.CitizenRepository.GetUserCitizenByUsernameAsync(phoneNumber);

                if (userCitizen == null)
                {
                    _logger.LogInformation("{Message}", "Citizen with phoneNumber: " + phoneNumber + "was not found");
                    return null;
                }

                citizenReadOnlyDTO = _mapper.Map<CitizenReadOnlyDTO>(userCitizen);
                _logger.LogInformation("{Message}", "Citizen with phoneNumber: " + phoneNumber + " was found");
            }
            catch (Exception ex)
            {

                _logger.LogError("{Message}{Excpetion}", ex.Message, ex.StackTrace);
            }

            return citizenReadOnlyDTO;
        }

        public async Task<CitizenDetailsReadOnlyDTO?> GetCitizenByUsernameAsync(string username)
        {
            CitizenDetailsReadOnlyDTO? citizenReadOnlyDTO = null;

            try
            {
                User? userCitizen = await _unitOfWork.CitizenRepository.GetUserCitizenByUsernameAsync(username);

                if (userCitizen == null)
                {
                    _logger.LogInformation("{Message}", "Citizen: " + username + "was not found");
                    return null; ;
                }

                citizenReadOnlyDTO = _mapper.Map<CitizenDetailsReadOnlyDTO>(userCitizen);
                _logger.LogInformation("{Message}", "Citizen: " + username + " was found");
            }
            catch (Exception ex)
            {

                _logger.LogError("{Message}{Excpetion}", ex.Message, ex.StackTrace);
            }

            return citizenReadOnlyDTO;
        }

        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        public async Task<List<CitizenDestinationsReadOnlyDTO>?> GetAllCitizenDestinationsFilteredAsync(Citizen citizen, CitizenDestinationFiltersDTO citizenDestinationFiltersDTO)
        {
            List<CitizenDestinationsReadOnlyDTO>? citizenDestinationsReadOnlyDTOs = new();
            try
            {
                Citizen? existingCitizen = await _unitOfWork.CitizenRepository.GetByIdAsync(citizen.Id);
                if (existingCitizen == null)
                {
                    _logger.LogInformation("{Message}", "Citizen with id: " + citizen.Id + " does not exist.");
                }
                List<Func<Destination, bool>> predicates = GetDestinationPredicates(citizenDestinationFiltersDTO);
                List<Destination> destinations = await _unitOfWork.CitizenRepository.GetCitizenDestinationsFilteredAsync(existingCitizen!.Id, predicates);

                if (destinations == null || !destinations.Any())
                {
                    _logger.LogInformation("{Message}", "No destinations found who fullfills the criteria for that citizen.");
                    return null; // Or return an empty list
                }

                citizenDestinationsReadOnlyDTOs = _mapper.Map<List<CitizenDestinationsReadOnlyDTO>>(destinations);

                _logger.LogInformation("{Message}", "Destinations who fullfills the criteria for that citizen retrieved");
            }
            catch (Exception ex)
            {

                _logger.LogError("{Message}{Excpetion}", ex.Message, ex.StackTrace);
            }
            return citizenDestinationsReadOnlyDTOs;

        }

        public async Task<List<CitizenReadOnlyDTO>> GetAllDestinationCitizensFilteredAsync(Destination destination, CitizenRoleDetailsFiltersDTO citizenRoleDetailsFiltersDTO)
        {
            List<CitizenReadOnlyDTO> citizenReadOnlyDTOs = new();
            try
            {
                Destination? existingDestination = await _unitOfWork.DestinationRepository.GetByIdAsync(destination.Id);
                if (existingDestination == null)
                {
                    _logger.LogInformation("{Message}", "Destination with id: " + destination.Id + " does not exist.");
                    return null;
                }

                List<Func<Citizen, bool>> predicates = GetCitizenPredicates(citizenRoleDetailsFiltersDTO);

                List<Citizen>? citizens = await _unitOfWork.CitizenRepository.GetCitizensDestinationFilteredAsync(existingDestination!.Id, predicates);
                if (citizens == null || !citizens.Any())
                {
                    _logger.LogInformation("{Message}", "No citizens found who fullfills the criteria for that destination.");
                    return null; // Or return an empty list
                }

                citizenReadOnlyDTOs = _mapper.Map<List<CitizenReadOnlyDTO>>(citizens);

                _logger.LogInformation("{Message}", "All agents who fullfills the criteria for that destination returned");


            }
            catch (Exception ex)
            {

                _logger.LogError("{Message}{Excpetion}", ex.Message, ex.StackTrace);
            }
            return citizenReadOnlyDTOs;
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        private List<Func<User, bool>> GetUserPredicates(CitizenDetailsFiltersDTO citizenDetaisFiltersDTO)
        {
            List<Func<User, bool>> predicates = new();

            if (!string.IsNullOrEmpty(citizenDetaisFiltersDTO.Username))
            {
                predicates.Add(u => u.Username == citizenDetaisFiltersDTO.Username);
            }
            if (!string.IsNullOrEmpty(citizenDetaisFiltersDTO.Email))
            {
                predicates.Add(u => u.Email == citizenDetaisFiltersDTO.Email);
            }
            if (!string.IsNullOrEmpty(citizenDetaisFiltersDTO.Firstname))
            {
                predicates.Add(u => u.Firstname == citizenDetaisFiltersDTO.Firstname);
            }
            if (!string.IsNullOrEmpty(citizenDetaisFiltersDTO.Lastname))
            {
                predicates.Add(u => u.Lastname == citizenDetaisFiltersDTO.Lastname);
            }
            if (!string.IsNullOrEmpty(citizenDetaisFiltersDTO.PhoneNumber))
            {
                predicates.Add(u => u.Details!.PhoneNumber == citizenDetaisFiltersDTO.PhoneNumber);
            }
            if (!string.IsNullOrEmpty(citizenDetaisFiltersDTO.Country))
            {
                predicates.Add(u => u.Details!.Country == citizenDetaisFiltersDTO.Country);
            }
            if (!string.IsNullOrEmpty(citizenDetaisFiltersDTO.City))
            {
                predicates.Add(u => u.Details!.City == citizenDetaisFiltersDTO.City);
            }
            return predicates;
        }

        private List<Func<Citizen, bool>> GetCitizenPredicates(CitizenRoleDetailsFiltersDTO citizenRoleDetailsFiltersDTO)
        {
            List<Func<Citizen, bool>> predicates = new();

            if (!string.IsNullOrEmpty(citizenRoleDetailsFiltersDTO.Username))
            {
                predicates.Add(u => u.User.Username == citizenRoleDetailsFiltersDTO.Username);
            }
            if (!string.IsNullOrEmpty(citizenRoleDetailsFiltersDTO.Email))
            {
                predicates.Add(u => u.User.Email == citizenRoleDetailsFiltersDTO.Email);
            }
            if (!string.IsNullOrEmpty(citizenRoleDetailsFiltersDTO.Firstname))
            {
                predicates.Add(u => u.User.Firstname == citizenRoleDetailsFiltersDTO.Firstname);
            }
            if (!string.IsNullOrEmpty(citizenRoleDetailsFiltersDTO.Lastname))
            {
                predicates.Add(u => u.User.Lastname == citizenRoleDetailsFiltersDTO.Lastname);
            }
            if (!string.IsNullOrEmpty(citizenRoleDetailsFiltersDTO.PhoneNumber))
            {
                predicates.Add(u => u.User.Details!.PhoneNumber == citizenRoleDetailsFiltersDTO.PhoneNumber);
            }
            if (!string.IsNullOrEmpty(citizenRoleDetailsFiltersDTO.Country))
            {
                predicates.Add(u => u.User.Details!.Country == citizenRoleDetailsFiltersDTO.Country);
            }
            if (!string.IsNullOrEmpty(citizenRoleDetailsFiltersDTO.City))
            {
                predicates.Add(u => u.User.Details!.City == citizenRoleDetailsFiltersDTO.City);
            }
            if (!string.IsNullOrEmpty(citizenRoleDetailsFiltersDTO.CitizenRole))
            {
                predicates.Add(u => u.CitizenDestinations!.Any(cd => cd.CitizenRole.ToString() == citizenRoleDetailsFiltersDTO.CitizenRole));
            }
            return predicates;
        }

        private List<Func<Destination, bool>> GetDestinationPredicates(CitizenDestinationFiltersDTO citizenDestinationFiltersDTO)
        {
            List<Func<Destination, bool>> predicates = new();

            if (!string.IsNullOrEmpty(citizenDestinationFiltersDTO.Country))
            {
                predicates.Add(d => d.Country == citizenDestinationFiltersDTO.Country);
            }
            if (!string.IsNullOrEmpty(citizenDestinationFiltersDTO.City))
            {
                predicates.Add(d => d.City == citizenDestinationFiltersDTO.City);
            }
            if (!string.IsNullOrEmpty(citizenDestinationFiltersDTO.Region))
            {
                predicates.Add(d => d.Region == citizenDestinationFiltersDTO.Region);
            }
            if (!string.IsNullOrEmpty(citizenDestinationFiltersDTO.Type))
            {
                predicates.Add(d => d.Type.ToString() == citizenDestinationFiltersDTO.Type);
            }
            if (!string.IsNullOrEmpty(citizenDestinationFiltersDTO.CitizenRole))
            {
                predicates.Add(d => d.CitizenDestinations!.Any(cd => cd.CitizenRole.ToString() == citizenDestinationFiltersDTO.CitizenRole));
            }
            return predicates;
        }
    }
}

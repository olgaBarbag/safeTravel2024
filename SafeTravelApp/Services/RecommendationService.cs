using AutoMapper;
using SafeTravelApp.Core.Enums;
using SafeTravelApp.Core.Filters;
using SafeTravelApp.Data;
using SafeTravelApp.DTO.Recommendation;
using SafeTravelApp.DTO.User;
using SafeTravelApp.Exceptions;
using SafeTravelApp.Repositories;
using Serilog;
using System.Security.Claims;

namespace SafeTravelApp.Services
{
    public class RecommendationService : IRecommendationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<RecommendationService> _logger;
        

        public RecommendationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = new LoggerFactory().AddSerilog().CreateLogger<RecommendationService>();
        }

        public async Task<Recommendation?> InsertRecommendationAsync(int userId, RecommendationInsertDTO insertDTO)
        {
            //var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            //var user = await _unitOfWork.UserRepository.GetByIdAsync(int.Parse(userId!));
            Recommendation recommendation;

            try
            {
                Destination? destination = await _unitOfWork.DestinationRepository.GetByIdAsync(insertDTO.DestinationId);
                if (destination == null)
                {
                    _logger.LogWarning("{Message}", "Destination with id: " + insertDTO.DestinationId + " not found");
                    return null;
                }

                Category? category = await _unitOfWork.CategoryRepository.GetByIdAsync(insertDTO.CategoryId);
                if (category == null)
                {
                    _logger.LogWarning("{Message}", "Category with id: " + insertDTO.CategoryId + " not found");
                    return null;
                }

                User? user = await _unitOfWork.UserRepository.GetByIdAsync(userId);
                if (user == null)
                {
                    _logger.LogWarning("{Message}","User with id: " + userId + " not found");
                    return null;
                }

                recommendation = new Recommendation
                {
                    User = user,
                    ContributorId = userId,
                };
                
                recommendation = _mapper.Map<Recommendation>(insertDTO);
                recommendation.ContributorRole = await _unitOfWork.RecommendationRepository.GetContributorRole(user, destination);

                await _unitOfWork.RecommendationRepository.AddAsync(recommendation);
                await _unitOfWork.SaveAsync();
                _logger!.LogInformation("{Message}", "Recommendation: " + recommendation.Title + " added successfully");
            }
            catch (Exception e)
            {
                _logger!.LogError("{Message}{Exception}", e.Message, e.StackTrace);
                throw;
            }
            return recommendation;
        }

        public async Task<Recommendation?> UpdateRecommendationAsync(int userId, int recommendationId, RecommendationUpdateDTO updateDTO)
        {
            Recommendation? updatedRecommendation;

            try
            {
                User? user = await _unitOfWork.UserRepository.GetByIdAsync(userId);
                if (user == null)
                {
                    _logger.LogWarning("{Message}", "User with id: " + userId + " not found");
                    return null;
                }
                updatedRecommendation = await _unitOfWork.RecommendationRepository.GetByIdAsync(recommendationId);
                if (updatedRecommendation == null)
                {
                    _logger.LogWarning("{Message}", "Recommendation with id: " + recommendationId + " not found");
                    return null;
                }
                updatedRecommendation = _mapper!.Map<Recommendation>(updateDTO);
                await _unitOfWork.RecommendationRepository.UpdateRecommendationAsync(userId, updatedRecommendation);
                await _unitOfWork.SaveAsync();
                _logger!.LogInformation("{Message}", "Recommendation: " + updateDTO.Title + " updated successfully");
            }
            catch (Exception e)
            {
                _logger!.LogError("{Message}{Exception}", e.Message, e.StackTrace);
                throw;
            }

            return updatedRecommendation;
        }
        public async Task DeleteRecommendationAsync(int id)
        {
            bool deleted;
            try
            {

                deleted = await _unitOfWork!.RecommendationRepository.DeleteAsync(id);

                if (!deleted)
                {
                    _logger.LogWarning("{Message}", "Recommendation was not found");
                }

                await _unitOfWork.SaveAsync();
                _logger!.LogInformation("{Message}", "Recommendation with id: " + id + " deleted successfully");

            }
            catch (Exception e)
            {
                _logger!.LogError("{Message}{Exception}", e.Message, e.StackTrace);
                throw;
            }
        }

        public Task<List<DestinationRecommendationReadOnlyDTO>> GetRecommendationsByContributorIdAsync(int contributorId)
        {
            throw new NotImplementedException();
        }
                
        public Task<List<DestinationRecommendationReadOnlyDTO>> GetRecommendationsByDestinationAsync(RecommendationFiltersDTO recommendationFiltersDTO)
        {
            throw new NotImplementedException();
        }


        //private async Task<User?> GetUserAsync()
        //{
        //    try
        //    {
        //        // Ανάκτηση του userId από τα claims
        //        var userIdString = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        //        if (string.IsNullOrEmpty(userIdString))
        //        {
        //            _logger.LogWarning("User ID not found in claims.");
        //            return null;
        //        }

        //        // Μετατροπή σε ακέραιο
        //        if (!int.TryParse(userIdString, out var userId))
        //        {
        //            _logger.LogWarning("Invalid User ID format in claims: {UserIdString}", userIdString);
        //            return null;
        //        }

        //        // Ανάκτηση του χρήστη από το repository
        //        var user = await _unitOfWork!.UserRepository.GetByIdAsync(userId);

        //        if (user == null)
        //        {
        //            _logger.LogWarning("User with ID {UserId} not found in database.", userId);
        //        }

        //        return user;
        //    }
        //    catch (Exception ex)
        //    {
        //        // Logging σε περίπτωση σφάλματος
        //        _logger.LogError(ex, "An error occurred while retrieving the user.");
        //        return null;
        //    }
        //}
    }


    }

using AutoMapper;
using SafeTravelApp.Core.Enums;
using SafeTravelApp.Core.Filters;
using SafeTravelApp.Data;
using SafeTravelApp.DTO.Citizen;
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

        public async Task<List<DestinationRecommendationReadOnlyDTO>> GetRecommendationsByContributorIdAsync(int contributorId)
        {
            List<DestinationRecommendationReadOnlyDTO>? destinationRecommendationReadOnlyDTOs = new();

            try
            {
               
                List<Recommendation>? recommendations = await _unitOfWork.RecommendationRepository.GetRecommendationsByContributorId(contributorId);

                if (recommendations == null)
                {
                    _logger.LogInformation("{Message}", "Recommendations by contributor : " + contributorId + "weren't found");
                    return null; ;
                }

                destinationRecommendationReadOnlyDTOs = _mapper.Map<List<DestinationRecommendationReadOnlyDTO>>(recommendations);

                _logger.LogInformation("{Message}", "Recommendations by contributor : " + contributorId + "were retrieved successfully");
            }
            catch (Exception ex)
            {

                _logger.LogError("{Message}{Excpetion}", ex.Message, ex.StackTrace);
            }

            return destinationRecommendationReadOnlyDTOs;
        }
                
        public async Task<List<DestinationRecommendationReadOnlyDTO>> GetRecommendationsByDestinationAsync(RecommendationFiltersDTO recommendationFiltersDTO)
        {
            List<DestinationRecommendationReadOnlyDTO>? destinationRecommendationReadOnlyDTOs = new();

            try
            {
                List<Func<Destination, bool>> predicates = GetDestinationRecommendationPredicates(recommendationFiltersDTO);

                List<Recommendation>? recommendations = await _unitOfWork.RecommendationRepository.GetRecommendationsByDestinationFiltered(predicates);

                if (recommendations == null)
                {
                    _logger.LogInformation("{Message}", "Recommendations weren't found");
                    return null; ;
                }

                destinationRecommendationReadOnlyDTOs = _mapper.Map<List<DestinationRecommendationReadOnlyDTO>>(recommendations);

                _logger.LogInformation("{Message}", "Recommendations retrieved successfully");
            }
            catch (Exception ex)
            {

                _logger.LogError("{Message}{Excpetion}", ex.Message, ex.StackTrace);
            }

            return destinationRecommendationReadOnlyDTOs;
        }

        public async Task<Recommendation?> GetRecommendationByIdAsync(int id)
        {
            Recommendation? recommendation = null;
            try
            {
                recommendation = await _unitOfWork.RecommendationRepository.GetByIdAsync(id);

                if (recommendation == null)
                {
                    _logger.LogInformation("{Message}", "Recommendation wasn't found");
                    return null; ;
                }

                _logger.LogInformation("{Message}", "Recommendations retrieved successfully");
            }
            catch (Exception ex)
            {

                _logger.LogError("{Message}{Excpetion}", ex.Message, ex.StackTrace);
            }

            return recommendation;
        }

        private List<Func<Destination, bool>> GetDestinationRecommendationPredicates(RecommendationFiltersDTO recommendationFiltersDTO)
        {
            List<Func<Destination, bool>> predicates = new();

            if (!string.IsNullOrEmpty(recommendationFiltersDTO.Country))
            {
                predicates.Add(d => d.Country == recommendationFiltersDTO.Country);
            }
            if (!string.IsNullOrEmpty(recommendationFiltersDTO.City))
            {
                predicates.Add(d => d.City == recommendationFiltersDTO.City);
            }
            if (!string.IsNullOrEmpty(recommendationFiltersDTO.Title))
            {
                predicates.Add(d => d.Recommendations!.Any(r => r.Title == recommendationFiltersDTO.Title));
            }
            if (!string.IsNullOrEmpty(recommendationFiltersDTO.DestinationCategory))
            {
                predicates.Add(d => d.Categories!.Any(c => c.DestinationCategory.ToString() == recommendationFiltersDTO.DestinationCategory));
            }
            return predicates;
        }
        
    }


    }

using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SafeTravelApp.Data;
using SafeTravelApp.DTO.Citizen;
using SafeTravelApp.DTO.Recommendation;
using SafeTravelApp.Exceptions;
using SafeTravelApp.Services;

namespace SafeTravelApp.Controllers
{
    [ApiController]
    [Route("api/citizens")]
    [Authorize(Roles = "Citizen")]
    public class CitizenController : BaseController
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public CitizenController(IApplicationService applicationService, IConfiguration configuration,
            IMapper mapper)
            : base(applicationService)
        {
            _configuration = configuration;
            _mapper = mapper;
        }

        [HttpGet("profile")]
        public async Task<ActionResult<CitizenReadOnlyDTO>> GetCitizenProfile()
        {
            var userId = AppUser!.Id;

            User? user = await _applicationService.UserService.GetUserByIdAsync(userId);
            if (user == null) return NotFound();

            var citizenId = user.Citizen.Id;

            var returnedCitizenDTO = await _applicationService.CitizenService.GetCitizenByIdAsync(citizenId);
            if (returnedCitizenDTO == null) return NotFound("Citizen profile not found.");

            return Ok(returnedCitizenDTO);
        }

        [HttpGet("profile/{username}")]
        public async Task<ActionResult<CitizenReadOnlyDTO>> GetCitizenByUsername()
        {
            var userId = AppUser!.Id;

            User? user = await _applicationService.UserService.GetUserByIdAsync(userId);
            if (user == null) return NotFound();

            var returnedCitizenDTO = await _applicationService.CitizenService.GetCitizenByUsernameAsync(user.Username);
            if (returnedCitizenDTO == null) return NotFound("Citizen profile not found.");

            return Ok(returnedCitizenDTO);
        }

        [HttpGet("recommendations")]
        public async Task<IActionResult> GetCitizenRecommendations()
        {
            var userId = AppUser!.Id;

            var returnedRecommendationsDTO = await _applicationService.RecommendationService.GetRecommendationsByContributorIdAsync(userId);

            return Ok(returnedRecommendationsDTO);
        }

        [HttpGet("recommendations/{recommendationId}")]
        public async Task<ActionResult<DestinationRecommendationReadOnlyDTO>> GetRecommendationById(int recommendationId)
        {
            var userId = AppUser!.Id;

            var recommendation = await _applicationService.RecommendationService.GetRecommendationByIdAsync(recommendationId);
            if (recommendation == null || recommendation.ContributorId != userId)
            {
                return Forbid("You do not have access to this recommendation.");
            }

            var returnedRecommendationDto = _mapper.Map<DestinationRecommendationReadOnlyDTO>(recommendation);
            return Ok(returnedRecommendationDto);
        }

        [HttpPost("recommendations")]
        public async Task<ActionResult<DestinationRecommendationReadOnlyDTO>> InsertRecommendation(RecommendationInsertDTO insertDTO)
        {
            var userId = AppUser!.Id;
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(e => e.Value!.Errors.Any())
                    .Select(e => new
                    {
                        Field = e.Key,
                        Errors = e.Value!.Errors.Select(error => error.ErrorMessage).ToArray()
                    });

                throw new InvalidRegistrationException("ErrorsInRegistation: " + errors);
            }

            var recommendation = await _applicationService.RecommendationService.InsertRecommendationAsync(userId, insertDTO);

            if (recommendation == null)
            {
                return BadRequest("Failed to insert the recommendation.");
            }

            var recommendationDto = _mapper.Map<DestinationRecommendationReadOnlyDTO>(recommendation);

            return CreatedAtAction(nameof(GetRecommendationById), new { recommendationId = recommendation.Id }, recommendationDto);
        }

        [HttpPut("recommendations/{recommendationId}")]
        public async Task<ActionResult<DestinationRecommendationReadOnlyDTO>> UpdateRecommendation(int recommendationId, RecommendationUpdateDTO updateDTO)
        {
            var userId = AppUser!.Id;

            var existingRecommendation = await _applicationService.RecommendationService.GetRecommendationByIdAsync(recommendationId);
            if (existingRecommendation is null)
            {
                throw new EntityNotFoundException("Recommendation", "Recommendation with id: " + recommendationId + " not found");
            }

            if (existingRecommendation.ContributorId != userId)
            {
                throw new EntityNotAuthorizedException("Recommendation", "ForbiddenAccess");
            }

            existingRecommendation = await _applicationService.RecommendationService.UpdateRecommendationAsync(userId, recommendationId, updateDTO);

            var updatedDto = _mapper.Map<DestinationRecommendationReadOnlyDTO>(existingRecommendation);
            return Ok(updatedDto);
        }

        [HttpDelete("recommendations/{recommendationId}")]
        public async Task<IActionResult> DeleteRecommendation(int recommendationId)
        {
            var userId = AppUser!.Id;

            var existingRecommendation = await _applicationService.RecommendationService.GetRecommendationByIdAsync(recommendationId);
            if (existingRecommendation is null)
            {
                throw new EntityNotFoundException("Recommendation", "Recommendation with id: " + recommendationId + " not found");
            }

            if (existingRecommendation.ContributorId != userId)
            {
                throw new EntityNotAuthorizedException("Recommendation", "ForbiddenAccess");
            }

            await _applicationService.RecommendationService.DeleteRecommendationAsync(recommendationId);

            return NoContent();
        }

    }
}

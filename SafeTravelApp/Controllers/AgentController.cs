using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SafeTravelApp.Data;
using SafeTravelApp.DTO.Agent;
using SafeTravelApp.DTO.Recommendation;
using SafeTravelApp.Exceptions;
using SafeTravelApp.Services;
using System.Security.Claims;

namespace SafeTravelApp.Controllers
{
    [ApiController]
    [Route("api/agents")]
    [Authorize(Roles = "Agent")]
    public class AgentController : BaseController
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AgentController(IApplicationService applicationService, IConfiguration configuration,
            IMapper mapper)
            : base(applicationService)
        {
            _configuration = configuration;
            _mapper = mapper;
        }

        
        [HttpGet("profile")]
        public async Task<ActionResult<AgentReadOnlyDTO>> GetAgentProfile()
        {
            var userId = AppUser!.Id;
            
            User? user = await _applicationService.UserService.GetUserByIdAsync(userId);
            if (user == null) return NotFound();
            
            var agentId = user.Agent.Id;
            
            var returnedAgentDTO = await _applicationService.AgentService.GetAgentByIdAsync(agentId);
            if (returnedAgentDTO == null) return NotFound("Agent profile not found.");

           
            return Ok(returnedAgentDTO);
        }

        [HttpGet("profile/{username}")]
        public async Task<ActionResult<AgentReadOnlyDTO>> GetAgentByUsername()
        {
            var userId = AppUser!.Id;

            User? user = await _applicationService.UserService.GetUserByIdAsync(userId);
            if (user == null) return NotFound();
            
            var returnedAgentDTO = await _applicationService.AgentService.GetAgentByUsernameAsync(user.Username);
            if (returnedAgentDTO == null) return NotFound("Agent profile not found.");


            return Ok(returnedAgentDTO);
        }
        //Get recommendations by this agent
        [HttpGet("recommendations")]
        public async Task<IActionResult> GetAgentRecommendations()
        {
            var userId = AppUser!.Id;
            
            var returnedRecommendationsDTO = await _applicationService.RecommendationService.GetRecommendationsByContributorIdAsync(userId);
             
            return Ok(returnedRecommendationsDTO);
        }

        // Get specific recommendation by ID
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

        // POST api/agents/recommendations
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
            if (_applicationService == null)
            {
                throw new ServerException("ApplicationServiceNull", "Application Service is null");
            }
            var recommendation = await _applicationService.RecommendationService.InsertRecommendationAsync(userId, insertDTO);

            if (recommendation == null)
            {
                return BadRequest("Failed to insert the recommendation.");
            }

            var recommendationDto = _mapper.Map<DestinationRecommendationReadOnlyDTO>(recommendation);

            // Επιστρέφουμε την σύσταση που δημιουργήθηκε με status 201 Created
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
            
            var updatedDto = _mapper.Map<DestinationRecommendationReadOnlyDTO>(existingRecommendation); // Χρήση του IMapper
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

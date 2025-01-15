using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SafeTravelApp.Data;
using SafeTravelApp.DTO.User;
using SafeTravelApp.DTO;
using SafeTravelApp.Exceptions;
using SafeTravelApp.Services;
using SafeTravelApp.DTO.Agent;
using SafeTravelApp.DTO.Citizen;

namespace SafeTravelApp.Controllers
{
    public class UserController : BaseController
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public UserController(IApplicationService applicationService, IConfiguration configuration,
            IMapper mapper)
            : base(applicationService)
        {
            _configuration = configuration;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<UserReadOnlyDTO>> SignupUserAgentAsync(AgentSignUpDTO? agentSignUpDTO)
        {
            if (!ModelState.IsValid)
            {
                // If the model state is not valid, build a custom response
                var errors = ModelState
                    .Where(e => e.Value!.Errors.Any())
                    .Select(e => new
                    {
                        Field = e.Key,
                        Errors = e.Value!.Errors.Select(error => error.ErrorMessage).ToArray()
                    });

                // instead of return BadRequest(new { Errors = errors });
                throw new InvalidRegistrationException("ErrorsInRegistation: " + errors);
            }
            if (_applicationService == null)
            {
                throw new ServerException("ApplicationServiceNull", "Application Service is null");
            }
            User? user = await _applicationService.UserService.GetUserByUsernameAsync(agentSignUpDTO!.Username!);
            if (user is not null)
            {
                throw new EntityAlreadyExistsException("User", "User: " + user.Username + "already exists");
            }
            UserReadOnlyDTO returnedUserDTO = await _applicationService.AgentService.SignUpUserAsync(agentSignUpDTO);
            //if (returnedUser is null)
            //{
            //    throw new InvalidRegistrationException("InvalidRegistration");
            //}
            //var returnedUserDTO = _mapper.Map<UserReadOnlyDTO>(returnedUser);
            return CreatedAtAction(nameof(GetUserById), new { id = returnedUserDTO.Id }, returnedUserDTO);
        }

        [HttpPost]
        public async Task<ActionResult<UserReadOnlyDTO>> SignupUserCitizenAsync(CitizenSignUpDTO? citizenSignUpDTO)
        {
            if (!ModelState.IsValid)
            {
                // If the model state is not valid, build a custom response
                var errors = ModelState
                    .Where(e => e.Value!.Errors.Any())
                    .Select(e => new
                    {
                        Field = e.Key,
                        Errors = e.Value!.Errors.Select(error => error.ErrorMessage).ToArray()
                    });

                // instead of return BadRequest(new { Errors = errors });
                throw new InvalidRegistrationException("ErrorsInRegistation: " + errors);
            }
            if (_applicationService == null)
            {
                throw new ServerException("ApplicationServiceNull", "Application Service is null");
            }
            User? user = await _applicationService.UserService.GetUserByUsernameAsync(citizenSignUpDTO!.Username!);
            if (user is not null)
            {
                throw new EntityAlreadyExistsException("User", "User: " + user.Username + "already exists");
            }
            UserReadOnlyDTO returnedUserDTO = await _applicationService.CitizenService.SignUpUserAsync(citizenSignUpDTO);
            //if (returnedUser is null)
            //{
            //    throw new InvalidRegistrationException("InvalidRegistration");
            //}
            //var returnedUserDTO = _mapper.Map<UserReadOnlyDTO>(returnedUser);
            return CreatedAtAction(nameof(GetUserById), new { id = returnedUserDTO.Id }, returnedUserDTO);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<UserReadOnlyDTO>> GetUserById(int id)
        {
            var user = await _applicationService.UserService.GetUserByIdAsync(id) ?? throw new EntityNotFoundException("User", "User: " + id + " NotFound");
            var returnedDto = _mapper.Map<UserReadOnlyDTO>(user);
            return Ok(returnedDto);
        }

        [HttpGet("{username}")]
        public async Task<ActionResult<AgentReadOnlyDTO>> GetUserAgentByUsername(string? username)
        {
            var returnedUserAgentDTO = await _applicationService.AgentService.GetAgentByUsernameAsync(username!)
                ?? throw new EntityNotFoundException("User", "User with username : " + username + " not found");
            return Ok(returnedUserAgentDTO);
        }

        public async Task<ActionResult<AgentReadOnlyDTO>> GetUserCitizenByUsername(string? username)
        {
            var returnedUserCitizenDTO = await _applicationService.CitizenService.GetCitizenByUsernameAsync(username!)
                ?? throw new EntityNotFoundException("User", "User with username : " + username + " not found");
            return Ok(returnedUserCitizenDTO);
        }

        [HttpPost]
        public async Task<ActionResult<JwtTokenDTO>> LoginUserAsync(UserLoginDTO credentials)
        {
            var user = await _applicationService.UserService.VerifyAndGetUserAsync(credentials);
            if (user == null)
            {
                throw new EntityNotAuthorizedException("User", "BadCredentials");
            }

            var userToken = _applicationService.UserService.CreateUserToken(user.Id, user.Username!, user.Email!,
                user.UserRole, _configuration["Authentication:SecretKey"]!);

            JwtTokenDTO token = new()
            {
                Token = userToken
            };

            return Ok(token);
        }

        //[HttpPatch("{id}")]
        //[Authorize(Roles = "Agent")]
        //public async Task<ActionResult<UserDTO>> UpdateUserPatch(int id, UserPatchDTO patchDTO)
        //{
        //    var userId = AppUser!.Id;
        //    if (id != userId)
        //    {
        //        throw new ForbiddenException("ForbiddenAccess");
        //    }

        //    var user = await _applicationService.UserService.UpdateUserPatchAsync(userId, patchDTO);
        //    var userDTO = _mapper.Map<UserDTO>(user);
        //    return Ok(userDTO);
        //}

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<UserDTO>> UpdateUserAccount(int id, UserDTO? userDTO)
        {
            var userId = AppUser!.Id;
            if (id != userId)
            {
                throw new EntityNotAuthorizedException("User", "ForbiddenAccess");
            }
            User user = _mapper.Map<User>(userDTO);
            User? returnedUser = await _applicationService.UserService.GetUserByUsernameAsync(userDTO!.Username!);
            if (returnedUser is null)
            {
                throw new EntityNotFoundException("User", "User: " + user.Username + " not found");
            }
            await _applicationService.UserService.UpdateUserAsync(id, userDTO);
            var returnedUserDTO = _mapper.Map<UserDTO>(user);
            return Ok(returnedUserDTO);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            //var userId = AppUser!.Id;
            //if (id != userId)
            //{
            //    throw new EntityForbiddenException("User", "ForbiddenAccess");
            //}

            //await _applicationService.UserService.DeleteUserAsync(userId);
            await _applicationService.UserService.DeleteUserAsync(id);
            return NoContent();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using SafeTravelApp.Exceptions;
using SafeTravelApp.Models;
using SafeTravelApp.Services;
using Serilog;
using System.Security.Claims;

namespace SafeTravelApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        public readonly IApplicationService _applicationService;
        private readonly ILogger<BaseController> _logger;

        protected BaseController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
            _logger = new LoggerFactory().AddSerilog().CreateLogger<BaseController>();

        }

        private ApplicationUser? _appUser;

        protected ApplicationUser? AppUser
        {
            get
            {
                if (User?.Claims == null || !User.Claims.Any())
                {
                    _logger.LogWarning("No claims found for the current user.");
                    return null;
                }
                if (User != null && User.Claims != null && User.Claims.Any())
                {

                    var claimsTypes = User.Claims.Select(x => x.Type);
                    if (!claimsTypes.Contains(ClaimTypes.NameIdentifier))
                    {
                        return null;
                    }

                    var userClaimsId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    if (string.IsNullOrEmpty(userClaimsId))
                    {
                        _logger.LogWarning("Claim 'NameIdentifier' is missing.");
                        return null;
                    }

                    _ = int.TryParse(userClaimsId, out int id);

                    if (id == 0)
                    {
                        _logger.LogWarning($"Invalid 'NameIdentifier' claim value: {userClaimsId}");
                        return null;
                    }
                                        
                    _appUser = new ApplicationUser
                    {
                        Id = id
                    };

                    var userClaimsName = User.FindFirst(ClaimTypes.Name)?.Value;

                    _appUser.Username = userClaimsName!;
                    _appUser.Email = User.FindFirst(ClaimTypes.Email)?.Value;
                    return _appUser;
                }
                return null;
            }
        }
    }
}

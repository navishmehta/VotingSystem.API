using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VotingSystem.API.DTOs;
using VotingSystem.API.Services.Interfaces;

namespace VotingSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ElectionConfigController : ControllerBase
    {
        private readonly IElectionConfigService _electionConfigService;
        public ElectionConfigController(IElectionConfigService electionConfigService)
        {
            _electionConfigService = electionConfigService;
        }

        [HttpGet]
        public IActionResult GetElectionConfig()
        {
            try
            {
                var config = _electionConfigService.GetElectionConfig();
                return Ok(config);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error occurred while fetching the election configuration." });
            }
        }

        [Authorize]
        [HttpPost]
        public IActionResult SetElectionConfig([FromBody] ElectionConfigDto configdto)
        {
            if (configdto == null)
                return BadRequest(new { message = "Invalid election configuration data." });

            try
            {
                _electionConfigService.SetElectionConfig(configdto.StartTime, configdto.EndTime);
                return Ok(new { message = "Election configuration set successfully." });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error occurred while setting the election configuration." });
            }
        }

        [HttpGet("current-time")]
        public IActionResult GetCurrentTime()
        {
            return Ok(new { utcTime = DateTime.UtcNow });
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using VotingSystem.API.Services.Interfaces;

namespace VotingSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateResultController : ControllerBase
    {
        private readonly IStateResultService _stateResultService;

        public StateResultController(IStateResultService stateResultService)
        {
            _stateResultService = stateResultService;
        }

        [HttpGet]
        public IActionResult GetStateResults()
        {
            try
            {
                var results = _stateResultService.GetStateResults();
                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpGet("{stateId}")]
        public IActionResult GetStateResultById(int stateId)
        {
            try
            {
                var result = _stateResultService.GetStateResultById(stateId);
                return Ok(result);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("State result not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}

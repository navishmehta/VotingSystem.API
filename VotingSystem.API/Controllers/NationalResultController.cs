using Microsoft.AspNetCore.Mvc;
using VotingSystem.API.Services.Interfaces;

namespace VotingSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NationalResultController : ControllerBase
    {
        private readonly INationalResultService _nationalResultService;

        public NationalResultController(INationalResultService nationalResultService)
        {
            _nationalResultService = nationalResultService;
        }

        [HttpGet]
        public IActionResult GetNationalResult()
        {
            try
            {
                var result = _nationalResultService.GetNationalResult();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}

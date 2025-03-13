using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VotingSystem.API.DTOs.VoterDtos;
using VotingSystem.API.Services.Interfaces;

namespace VotingSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoterController : ControllerBase
    {
        private readonly IVoterService _voterService;

        public VoterController(IVoterService voterService)
        {
            _voterService = voterService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var voters = _voterService.GetAll();
                return Ok(voters);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while fetching voters.", error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var voter = _voterService.GetById(id);
                return Ok(voter);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while fetching the voter.", error = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody] VoterRequestDto voterdto)
        {
            try
            {
                var createdVoter = _voterService.Create(voterdto);
                return StatusCode(201, new
                {
                    message = "Voter created successfully.",
                    voter = createdVoter
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An error occurred while creating the voter.",
                    error = ex.InnerException?.Message ?? ex.Message
                });
            }
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] VoterRequestDto voterdto)
        {
            try
            {
                var updatedVoter = _voterService.Update(id, voterdto);
                return Ok(new
                {
                    message = "Voter updated successfully.",
                    voter = updatedVoter
                });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating the voter.", error = ex.Message });
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var deletedVoter = _voterService.Delete(id);
                return Ok(new
                {
                    message = "Voter deleted successfully.",
                    voter = deletedVoter
                });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while deleting the voter.", error = ex.Message });
            }
        }
    }
}

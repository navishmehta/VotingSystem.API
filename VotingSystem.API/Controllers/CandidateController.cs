using Microsoft.AspNetCore.Mvc;
using VotingSystem.API.Services.Interfaces;
using System;
using VotingSystem.API.DTOs.CandidateDtos;
using Microsoft.AspNetCore.Authorization;

namespace VotingSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        private readonly ICandidateService _candidateService;

        public CandidateController(ICandidateService candidateService)
        {
            _candidateService = candidateService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var candidates = _candidateService.GetAll();
                return Ok(candidates);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "An internal server error occurred." });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var candidate = _candidateService.GetById(id);
                return Ok(candidate);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving the candidate.", error = ex.Message });
            }
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create([FromBody] CandidateRequestDto candidatedto)
        {
            try
            {
                _candidateService.Create(candidatedto);
                return StatusCode(201, new { message = "Candidate created successfully." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while creating the candidate.", error = ex.Message });
            }
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] CandidateRequestDto candidatedto)
        {
            try
            {
                _candidateService.Update(id, candidatedto);
                return Ok(new { message = "Candidate updated successfully." });
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
                return StatusCode(500, new { message = "An error occurred while updating the candidate.", error = ex.Message });
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _candidateService.Delete(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while deleting the candidate.", error = ex.Message });
            }
        }
    }
}

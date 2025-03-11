using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using VotingSystem.API.DTOs.VoteDtos;
using VotingSystem.API.Services.Interfaces;

namespace VotingSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoteController : ControllerBase
    {
        private readonly IVoteService _voteService;

        public VoteController(IVoteService voteService)
        {
            _voteService = voteService;
        }

        // Get All Votes
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var votes = _voteService.GetAll();
                return Ok(votes);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "An unexpected error occurred while retrieving votes." });
            }
        }

        // Get Vote by Id
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var vote = _voteService.GetById(id);
                return Ok(vote);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "An unexpected error occurred while retrieving the vote." });
            }
        }

        // Create a new Vote
        [HttpPost]
        public IActionResult Create([FromBody] VoteRequestDto votedto)
        {
            if (votedto == null)
                return BadRequest(new { message = "Invalid vote data." });

            try
            {
                _voteService.Create(votedto);
                return StatusCode(201, new { message = "Vote recorded successfully." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "An unexpected error occurred while recording the vote." });
            }
        }

        // Delete a Vote
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _voteService.Delete(id);
                return Ok(new { message = "Vote deleted successfully" });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "An unexpected error occurred while deleting the vote." });
            }
        }
    }
}
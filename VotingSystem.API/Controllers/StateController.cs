using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VotingSystem.API.Services.Interfaces;
using System;
using VotingSystem.API.DTOs.StateDtos;

namespace VotingSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateController : ControllerBase
    {
        private readonly IStateService _stateService;

        public StateController(IStateService stateService)
        {
            _stateService = stateService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var states = _stateService.GetAllStates();
                return Ok(states);
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
                var state = _stateService.GetById(id);
                return Ok(state);
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

        [Authorize]
        [HttpPost]
        public IActionResult Create([FromBody] StateRequestDto statedto)
        {
            if (statedto == null || string.IsNullOrWhiteSpace(statedto.Name))
            {
                return BadRequest(new { message = "State name is required." });
            }

            try
            {
                _stateService.Create(statedto);
                return StatusCode(201, new { message = "State created successfully." });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "An internal server error occurred." });
            }
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] StateRequestDto statedto)
        {
            if (statedto == null || string.IsNullOrWhiteSpace(statedto.Name))
            {
                return BadRequest(new { message = "State name is required." });
            }

            try
            {
                _stateService.Update(id, statedto);
                return Ok(new { message = "State updated successfully." });
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
                return StatusCode(500, new { message = "An internal server error occurred." });
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _stateService.Delete(id);
                return Ok(new { message = "State deleted successfully" });
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
    }
}

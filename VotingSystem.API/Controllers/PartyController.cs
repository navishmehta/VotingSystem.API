using Microsoft.AspNetCore.Mvc;
using VotingSystem.API.Services.Interfaces;
using VotingSystem.API.DTOs.PartyDtos;
using Microsoft.AspNetCore.Authorization;

namespace VotingSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartyController : ControllerBase
    {
        private readonly IPartyService _partyService;

        public PartyController(IPartyService partyService)
        {
            _partyService = partyService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var parties = _partyService.GetAllParties();
                return Ok(parties);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch
            {
                return StatusCode(500, new { message = "An internal server error occurred while fetching parties." });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var party = _partyService.GetById(id);
                return Ok(party);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch
            {
                return StatusCode(500, new { message = "An internal server error occurred while fetching the party." });
            }
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create([FromBody] PartyRequestDto partydto)
        {
            if (partydto == null || string.IsNullOrWhiteSpace(partydto.Name) || string.IsNullOrWhiteSpace(partydto.Symbol))
            {
                return BadRequest(new { message = "Party name and symbol are required." });
            }

            try
            {
                _partyService.Create(partydto);
                return StatusCode(201, new { message = "Party created successfully." });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch
            {
                return StatusCode(500, new { message = "An internal server error occurred while creating the party." });
            }
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] PartyRequestDto partydto)
        {
            if (partydto == null || (string.IsNullOrWhiteSpace(partydto.Name) && string.IsNullOrWhiteSpace(partydto.Symbol)))
            {
                return BadRequest(new { message = "At least one field (Name or Symbol) must be provided for update." });
            }

            try
            {
                _partyService.Update(id, partydto);
                return Ok(new { message = "Party updated successfully." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch
            {
                return StatusCode(500, new { message = "An internal server error occurred while updating the party." });
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _partyService.Delete(id);
                return Ok(new { message = "Party deleted successfully." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch
            {
                return StatusCode(500, new { message = "An internal server error occurred while deleting the party." });
            }
        }
    }
}

using VotingSystem.API.Data;
using VotingSystem.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using VotingSystem.API.DTOs.ResultDtos;

namespace VotingSystem.API.Services
{
    public class NationalResultService : INationalResultService
    {
        private readonly VotingSystemDbContext _context;

        public NationalResultService(VotingSystemDbContext context)
        {
            _context = context;
        }

        public NationalResultDto GetNationalResult()
        {
            try
            {
                var stateWinners = _context.Votes
                    .Include(v => v.Candidate)
                    .ThenInclude(c => c.Party)
                    .Include(v => v.Candidate.State)
                    .GroupBy(v => v.Candidate.StateId)
                    .Select(stateGroup => new
                    {
                        StateId = stateGroup.Key,
                        WinningPartyId = stateGroup
                            .GroupBy(v => v.Candidate.PartyId)
                            .OrderByDescending(partyGroup => partyGroup.Count())
                            .First().Key
                    })
                    .ToList();

                var nationalResults = stateWinners
                    .GroupBy(sw => sw.WinningPartyId)
                    .Select(partyGroup => new
                    {
                        PartyId = partyGroup.Key,
                        StatesWon = partyGroup.Count()
                    })
                    .OrderByDescending(p => p.StatesWon)
                    .FirstOrDefault();

                if (nationalResults == null)
                    throw new Exception("No election data available.");

                var winningParty = _context.Parties.Find(nationalResults.PartyId);

                return new NationalResultDto
                {
                    PartyId = winningParty.Id,
                    PartyName = winningParty.Name,
                    StatesWon = nationalResults.StatesWon
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving national result: {ex.Message}");
            }
        }
    }
}

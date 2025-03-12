using VotingSystem.API.Data;
using VotingSystem.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using VotingSystem.API.DTOs.ResultDtos;

namespace VotingSystem.API.Services
{
    public class StateResultService : IStateResultService
    {
        private readonly VotingSystemDbContext _context;

        public StateResultService(VotingSystemDbContext context)
        {
            _context = context;
        }

        public IEnumerable<StateResultDto> GetStateResults()
        {
            try
            {
                return _context.Votes
                    .Include(v => v.Candidate)
                    .ThenInclude(c => c.Party)
                    .Include(v => v.Candidate.State)
                    .GroupBy(v => new { v.Candidate.StateId, v.Candidate.PartyId })
                    .Select(group => new StateResultDto
                    {
                        StateId = group.Key.StateId,
                        StateName = group.First().Candidate.State.Name,
                        PartyId = group.Key.PartyId,
                        PartyName = group.First().Candidate.Party.Name,
                        VoteCount = group.Count()
                    })
                    .OrderBy(sr => sr.StateId)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving state results: {ex.Message}");
            }
        }

        public StateResultDto GetStateResultById(int stateId)
        {
            try
            {
                var stateResults = GetStateResults()
                    .Where(sr => sr.StateId == stateId)
                    .ToList();

                if (!stateResults.Any())
                {
                    throw new KeyNotFoundException("State result not found.");
                }

                return stateResults.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving state result by ID: {ex.Message}");
            }
        }
    }
}

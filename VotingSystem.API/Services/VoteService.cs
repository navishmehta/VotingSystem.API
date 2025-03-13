using Microsoft.EntityFrameworkCore;
using VotingSystem.API.Data;
using VotingSystem.API.DTOs.VoteDtos;
using VotingSystem.API.Models;
using VotingSystem.API.Services.Interfaces;

namespace VotingSystem.API.Services
{
    public class VoteService : IVoteService
    {
        private readonly VotingSystemDbContext _context;

        public VoteService(VotingSystemDbContext context)
        {
            _context = context;
        }

        public IEnumerable<VoteResponseDto> GetAll()
        {
            var votes = _context.Votes
                .Include(v => v.Candidate)
                .ThenInclude(c => c.Party)
                .Select(v => new VoteResponseDto
                {
                    Id = v.Id,
                    VoterId = v.VoterId,
                    CandidateId = v.CandidateId,
                    CandidateName = v.Candidate != null ? v.Candidate.Name : null,
                    PartyName = v.Candidate != null && v.Candidate.Party != null ? v.Candidate.Party.Name : null
                })
                .ToList();

            if (!votes.Any())
                throw new KeyNotFoundException("No votes found.");

            return votes;
        }


        public VoteResponseDto GetById(int id)
        {
            var vote = _context.Votes
                .Include(v => v.Candidate)
                .ThenInclude(c => c.Party)
                .FirstOrDefault(v => v.Id == id);

            if (vote == null)
                throw new KeyNotFoundException("Vote not found.");

            return new VoteResponseDto
            {
                Id = vote.Id,
                VoterId = vote.VoterId,
                CandidateId = vote.CandidateId,
                CandidateName = vote.Candidate?.Name,
                PartyName = vote.Candidate?.Party?.Name
            };
        }


        public VoteResponseDto Create(VoteRequestDto votedto)
        {
            if (votedto == null)
                throw new ArgumentNullException(nameof(votedto), "Vote data cannot be null.");

            if (string.IsNullOrWhiteSpace(votedto.CandidateName) || string.IsNullOrWhiteSpace(votedto.PartyName))
                throw new ArgumentException("Both CandidateName and PartyName are required.");

            var config = _context.ElectionConfig.FirstOrDefault()
                ?? throw new InvalidOperationException("Election timing not set.");

            if (DateTime.UtcNow < config.StartTime)
                throw new InvalidOperationException("Voting has not started yet.");

            if (DateTime.UtcNow > config.EndTime)
                throw new InvalidOperationException("Voting has ended.");

            var voter = _context.Voters
                .Include(v => v.State)
                .FirstOrDefault(v => v.Id == votedto.VoterId)
                ?? throw new KeyNotFoundException("Voter does not exist.");

            if (_context.Votes.Any(v => v.VoterId == votedto.VoterId))
                throw new InvalidOperationException("This voter has already cast their vote and cannot vote again.");

            var candidate = _context.Candidates
                .Include(c => c.Party)
                .FirstOrDefault(c =>
                    string.Equals(c.Name, votedto.CandidateName, StringComparison.OrdinalIgnoreCase) &&
                    string.Equals(c.Party.Name, votedto.PartyName, StringComparison.OrdinalIgnoreCase) &&
                    c.StateId == voter.StateId);

            if (candidate == null)
                throw new KeyNotFoundException("No candidate found with the given name and party in the voter's state.");

            var vote = new Vote
            {
                VoterId = votedto.VoterId,
                CandidateId = candidate.Id
            };

            _context.Votes.Add(vote);
            _context.SaveChanges();

            return new VoteResponseDto
            {
                Id = vote.Id,
                VoterId = vote.VoterId,
                CandidateId = vote.CandidateId,
                CandidateName = candidate.Name,
                PartyName = candidate.Party.Name
            };
        }





        public VoteResponseDto Delete(int id)
        {
            var vote = _context.Votes
                .Include(v => v.Candidate)
                .ThenInclude(c => c.Party)
                .FirstOrDefault(v => v.Id == id);

            if (vote == null)
                throw new KeyNotFoundException("Vote not found.");

            var candidate = vote.Candidate ?? throw new InvalidOperationException("Candidate not found.");
            var party = candidate.Party ?? throw new InvalidOperationException("Party not found.");

            _context.Votes.Remove(vote);
            _context.SaveChanges();

            return new VoteResponseDto
            {
                Id = vote.Id,
                VoterId = vote.VoterId,
                CandidateId = vote.CandidateId,
                CandidateName = candidate.Name,
                PartyName = party.Name
            };
        }

    }
}

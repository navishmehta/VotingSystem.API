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
            var votes = _context.Votes.Select(v => new VoteResponseDto
            {
                Id = v.Id,
                VoterId = v.VoterId,
                CandidateId = v.CandidateId
            }).ToList();

            if (!votes.Any())
            {
                throw new KeyNotFoundException("No votes found.");
            }

            return votes;
        }

        public VoteResponseDto GetById(int id)
        {
            var vote = _context.Votes.Find(id);
            if (vote == null)
                throw new KeyNotFoundException("Vote not found.");

            return new VoteResponseDto
            {
                Id = vote.Id,
                VoterId = vote.VoterId,
                CandidateId = vote.CandidateId
            };
        }

        public void Create(VoteRequestDto votedto)
        {
            if (votedto == null)
                throw new ArgumentNullException("Vote data cannot be null.");
            
            var config = _context.ElectionConfig.FirstOrDefault();
            if (config == null)
                throw new InvalidOperationException("Election timing not set.");

            if (DateTime.UtcNow < config.StartTime)
                throw new InvalidOperationException("Voting has not started yet.");

            if (DateTime.UtcNow > config.EndTime)
                throw new InvalidOperationException("Voting has ended.");

            var voter = _context.Voters.Include(v => v.State).FirstOrDefault(v => v.Id == votedto.VoterId);
            var candidate = _context.Candidates.Include(c => c.State).FirstOrDefault(c => c.Id == votedto.CandidateId);

            if (voter == null)
                throw new KeyNotFoundException("Voter does not exist.");

            if (candidate == null)
                throw new KeyNotFoundException("Candidate does not exist.");
            
            if (voter.StateId != candidate.StateId)
                throw new InvalidOperationException("Voter and Candidate belongs to different state.");

            bool hasAlreadyVoted = _context.Votes.Any(v => v.VoterId == votedto.VoterId);
            if (hasAlreadyVoted)
                throw new InvalidOperationException("This voter has already cast their vote and cannot vote again.");

            var vote = new Vote
            {
                VoterId = votedto.VoterId,
                CandidateId = votedto.CandidateId
            };

            _context.Votes.Add(vote);
            _context.SaveChanges();
        }


        public void Delete(int id)
        {
            var vote = _context.Votes.Find(id);
            if (vote == null)
                throw new KeyNotFoundException("Vote not found.");

            _context.Votes.Remove(vote);
            _context.SaveChanges();
        }
    }
}

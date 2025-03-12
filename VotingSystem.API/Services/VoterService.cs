using VotingSystem.API.Data;
using VotingSystem.API.DTOs.Voter;
using VotingSystem.API.DTOs.VoterDtos;
using VotingSystem.API.Models;
using VotingSystem.API.Services.Interfaces;

namespace VotingSystem.API.Services
{
    public class VoterService : IVoterService
    {
        private readonly VotingSystemDbContext _context;

        public VoterService(VotingSystemDbContext context)
        {
            _context = context;
        }

        public IEnumerable<VoterResponseDto> GetAll()
        {
            var voters = _context.Voters.ToList();

            if (!voters.Any())
                throw new KeyNotFoundException("No voters found.");

            return voters.Select(v => new VoterResponseDto
            {
                Id = v.Id,
                Name = v.Name,
                StateId = v.StateId
            }).ToList();
        }


        public VoterResponseDto GetById(int id)
        {
            var voter = _context.Voters.Find(id)
                ?? throw new KeyNotFoundException("Voter not found.");

            return new VoterResponseDto
            {
                Id = voter.Id,
                Name = voter.Name,
                StateId = voter.StateId
            };
        }

        public void Create(VoterRequestDto voterdto)
        {
            if (voterdto == null || string.IsNullOrWhiteSpace(voterdto.Name))
                throw new ArgumentException("Voter name is required.");

            if (!voterdto.StateId.HasValue || voterdto.StateId.Value <= 0)
                throw new ArgumentException("Valid StateId is required.");            

            var voter = new Voter
            {
                Name = voterdto.Name.Trim(),
                StateId = voterdto.StateId.Value
            };

            _context.Voters.Add(voter);
            _context.SaveChanges();
        }

        public void Update(int id, VoterRequestDto voterdto)
        {
            var voter = _context.Voters.Find(id)
                ?? throw new KeyNotFoundException("Voter not found.");

            if (!string.IsNullOrWhiteSpace(voterdto.Name))
                voter.Name = voterdto.Name.Trim();

            if (voterdto.StateId.HasValue && voterdto.StateId.Value > 0)
                voter.StateId = voterdto.StateId.Value;            

            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var voter = _context.Voters.Find(id)
                ?? throw new KeyNotFoundException("Voter not found.");

            _context.Voters.Remove(voter);
            _context.SaveChanges();
        }
    }
}

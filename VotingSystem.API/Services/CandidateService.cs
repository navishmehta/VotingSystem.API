using VotingSystem.API.Data;
using VotingSystem.API.Models;
using VotingSystem.API.Services.Interfaces;
using VotingSystem.API.DTOs.CandidateDtos;

namespace VotingSystem.API.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly VotingSystemDbContext _context;

        public CandidateService(VotingSystemDbContext context)
        {
            _context = context;
        }

        public IEnumerable<CandidateResponseDto> GetAll()
        {
            var candidates = _context.Candidates
                .Select(c => new CandidateResponseDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    PartyId = c.PartyId,
                    StateId = c.StateId
                }).ToList();

            if (!candidates.Any())
            {
                throw new KeyNotFoundException("No candidates found.");
            }

            return candidates;
        }

        public CandidateResponseDto GetById(int id)
        {
            var candidate = _context.Candidates.Find(id)
                ?? throw new KeyNotFoundException("Candidate not found.");

            return new CandidateResponseDto
            {
                Id = candidate.Id,
                Name = candidate.Name,
                PartyId = candidate.PartyId,
                StateId = candidate.StateId
            };
        }

        public void Create(CandidateRequestDto candidatedto)
        {
            if (candidatedto == null || string.IsNullOrWhiteSpace(candidatedto.Name))
            {
                throw new ArgumentException("Candidate name is required.");
            }

            int partyId = candidatedto.PartyId ?? throw new ArgumentException("Valid PartyId is required.");
            if (partyId <= 0)
            {
                throw new ArgumentException("PartyId must be greater than zero.");
            }

            int stateId = candidatedto.StateId ?? throw new ArgumentException("Valid StateId is required.");
            if (stateId <= 0)
            {
                throw new ArgumentException("StateId must be greater than zero.");
            }

            bool candidateExists = _context.Candidates.Any(c => c.PartyId == partyId && c.StateId == stateId);

            if (candidateExists)
            {
                throw new InvalidOperationException("A candidate from this party already exists in this state.");
            }

            var candidate = new Candidate
            {
                Name = candidatedto.Name.Trim(),
                PartyId = partyId,
                StateId = stateId
            };

            _context.Candidates.Add(candidate);
            _context.SaveChanges();
        }

        public void Update(int id, CandidateRequestDto candidateDto)
        {
            var candidate = _context.Candidates.Find(id)
                ?? throw new KeyNotFoundException("Candidate not found.");

            if (!string.IsNullOrWhiteSpace(candidateDto.Name))
            {
                candidate.Name = candidateDto.Name.Trim();
            }

            int newPartyId = candidateDto.PartyId ?? candidate.PartyId;
            int newStateId = candidateDto.StateId ?? candidate.StateId;
            
            bool isDuplicate = _context.Candidates
                .Any(c => c.Id != id && c.PartyId == newPartyId && c.StateId == newStateId);

            if (isDuplicate)
            {
                throw new InvalidOperationException("A candidate from this party already exists in this state.");
            }

            candidate.PartyId = newPartyId;
            candidate.StateId = newStateId;

            _context.SaveChanges();
        }


        public void Delete(int id)
        {
            var candidate = _context.Candidates.Find(id)
                ?? throw new KeyNotFoundException("Candidate not found.");

            _context.Candidates.Remove(candidate);
            _context.SaveChanges();
        }
    }
}

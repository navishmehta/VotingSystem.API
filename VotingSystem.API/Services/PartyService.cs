using VotingSystem.API.Data;
using VotingSystem.API.Models;
using VotingSystem.API.Services.Interfaces;
using VotingSystem.API.DTOs.PartyDtos;

namespace VotingSystem.API.Services
{
    public class PartyService : IPartyService
    {
        private readonly VotingSystemDbContext _context;

        public PartyService(VotingSystemDbContext context)
        {
            _context = context;
        }

        public IEnumerable<PartyResponseDto> GetAllParties()
        {
            var parties = _context.Parties
                .Select(p => new PartyResponseDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Symbol = p.Symbol
                }).ToList();

            if (!parties.Any())
            {
                throw new KeyNotFoundException("No party found.");
            }

            return parties;
        }

        public PartyResponseDto GetById(int id)
        {
            var party = _context.Parties.Find(id);
            if (party == null)
            {
                throw new KeyNotFoundException("Party not found.");
            }

            return new PartyResponseDto
            {
                Id = party.Id,
                Name = party.Name,
                Symbol = party.Symbol
            };
        }

        public void Create(PartyRequestDto partydto)
        {
            if (partydto == null || string.IsNullOrWhiteSpace(partydto.Name) || string.IsNullOrWhiteSpace(partydto.Symbol))
            {
                throw new ArgumentException("Party name and symbol are required.");
            }

            if (_context.Parties.Any(p => p.Name.ToLower() == partydto.Name.ToLower()))
            {
                throw new InvalidOperationException("A party with this name already exists.");
            }

            if (_context.Parties.Any(p => p.Symbol.ToLower() == partydto.Symbol.ToLower()))
            {
                throw new InvalidOperationException("A party with this symbol already exists.");
            }

            var party = new Party
            {
                Name = partydto.Name.Trim(),
                Symbol = partydto.Symbol.Trim(),
            };

            _context.Parties.Add(party);
            _context.SaveChanges();
        }

        public void Update(int id, PartyRequestDto partydto)
        {
            var party = _context.Parties.Find(id);
            if (party == null)
            {
                throw new KeyNotFoundException("Party not found.");
            }

            if (!string.IsNullOrWhiteSpace(partydto.Name))
            {
                if (_context.Parties.Any(p => p.Name.ToLower() == partydto.Name.ToLower() && p.Id != id))
                {
                    throw new InvalidOperationException("A party with this name already exists.");
                }
                party.Name = partydto.Name.Trim();
            }

            if (!string.IsNullOrWhiteSpace(partydto.Symbol))
            {
                if (_context.Parties.Any(p => p.Symbol.ToLower() == partydto.Symbol.ToLower() && p.Id != id))
                {
                    throw new InvalidOperationException("A party with this symbol already exists.");
                }
                party.Symbol = partydto.Symbol.Trim();
            }

            _context.SaveChanges();
        }



        public void Delete(int id)
        {
            var party = _context.Parties.Find(id);
            if (party == null)
            {
                throw new KeyNotFoundException("Party not found.");
            }

            _context.Parties.Remove(party);
            _context.SaveChanges();
        }
    }
}

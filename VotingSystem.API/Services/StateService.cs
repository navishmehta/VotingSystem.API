using VotingSystem.API.Data;
using VotingSystem.API.DTOs.StateDtos;
using VotingSystem.API.Models;
using VotingSystem.API.Services.Interfaces;

namespace VotingSystem.API.Services
{
    public class StateService : IStateService
    {
        private readonly VotingSystemDbContext _context;

        public StateService(VotingSystemDbContext context)
        {
            _context = context;
        }

        public IEnumerable<StateResponseDto> GetAllStates()
        {
            var states = _context.States.Select(s => new StateResponseDto
            {
                Id = s.Id,
                Name = s.Name
            }).ToList();

            if (!states.Any())
            {
                throw new KeyNotFoundException("No states found in the database.");
            }

            return states;
        }

        public StateResponseDto GetById(int id)
        {
            var state = _context.States.Find(id);
            if (state == null)
            {
                throw new KeyNotFoundException("State not found.");
            }

            return new StateResponseDto
            {
                Id = state.Id,
                Name = state.Name
            };
        }

        public StateResponseDto Create(StateRequestDto statedto)
        {
            if (statedto == null || string.IsNullOrWhiteSpace(statedto.Name))
            {
                throw new ArgumentException("State name is required.");
            }

            if (_context.States.Any(s => s.Name.ToLower() == statedto.Name.Trim().ToLower()))
            {
                throw new InvalidOperationException("A state with this name already exists.");
            }

            var state = new State { Name = statedto.Name.Trim() };
            _context.States.Add(state);
            _context.SaveChanges();

            return new StateResponseDto
            {
                Id = state.Id,
                Name = state.Name
            };
        }

        public StateResponseDto Update(int id, StateRequestDto statedto)
        {
            var state = _context.States.Find(id);
            if (state == null)
            {
                throw new KeyNotFoundException("State not found.");
            }

            if (_context.States.Any(s => s.Name == statedto.Name.Trim() && s.Id != id))
            {
                throw new InvalidOperationException("A state with this name already exists.");
            }

            state.Name = statedto.Name.Trim();
            _context.SaveChanges();

            return new StateResponseDto
            {
                Id = state.Id,
                Name = state.Name
            };
        }

        public StateResponseDto Delete(int id)
        {
            var state = _context.States.Find(id);
            if (state == null)
            {
                throw new KeyNotFoundException("State not found.");
            }

            _context.States.Remove(state);
            _context.SaveChanges();

            return new StateResponseDto
            {
                Id = state.Id,
                Name = state.Name
            };
        }
    }
}

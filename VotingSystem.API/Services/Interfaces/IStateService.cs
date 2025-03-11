using VotingSystem.API.DTOs.StateDtos;

namespace VotingSystem.API.Services.Interfaces
{
    public interface IStateService
    {
        IEnumerable<StateResponseDto> GetAllStates();
        StateResponseDto GetById(int id);
        void Create(StateRequestDto statedto);
        void Update(int id, StateRequestDto statedto);
        void Delete(int id);
    }
}

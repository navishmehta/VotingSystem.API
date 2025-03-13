using VotingSystem.API.DTOs.StateDtos;

namespace VotingSystem.API.Services.Interfaces
{
    public interface IStateService
    {
        IEnumerable<StateResponseDto> GetAllStates();
        StateResponseDto GetById(int id);
        StateResponseDto Create(StateRequestDto statedto);
        StateResponseDto Update(int id, StateRequestDto statedto);
        StateResponseDto Delete(int id);
    }
}

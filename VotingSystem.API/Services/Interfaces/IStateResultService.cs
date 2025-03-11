using VotingSystem.API.DTOs;

namespace VotingSystem.API.Services.Interfaces
{
    public interface IStateResultService
    {
        IEnumerable<StateResultDto> GetStateResults();
        StateResultDto GetStateResultById(int stateId);
    }
}

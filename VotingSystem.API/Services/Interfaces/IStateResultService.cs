using VotingSystem.API.DTOs.ResultDtos;

namespace VotingSystem.API.Services.Interfaces
{
    public interface IStateResultService
    {
        IEnumerable<StateResultDto> GetStateResults();
        StateResultDto GetStateResultById(int stateId);
    }
}

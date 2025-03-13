using VotingSystem.API.DTOs.Voter;
using VotingSystem.API.DTOs.VoterDtos;

namespace VotingSystem.API.Services.Interfaces
{
    public interface IVoterService
    {
        IEnumerable<VoterResponseDto> GetAll();
        VoterResponseDto GetById(int id);
        VoterResponseDto Create(VoterRequestDto voterdto);
        VoterResponseDto Update(int id, VoterRequestDto voterdto);
        VoterResponseDto Delete(int id);
    }
}

using VotingSystem.API.DTOs.Voter;
using VotingSystem.API.DTOs.VoterDtos;

namespace VotingSystem.API.Services.Interfaces
{
    public interface IVoterService
    {
        IEnumerable<VoterResponseDto> GetAll();
        VoterResponseDto GetById(int id);
        void Create(VoterRequestDto voterdto);
        void Update(int id, VoterRequestDto voterdto);
        void Delete(int id);
    }
}

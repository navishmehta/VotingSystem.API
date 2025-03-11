using VotingSystem.API.DTOs.VoteDtos;

namespace VotingSystem.API.Services.Interfaces
{
    public interface IVoteService
    {
        IEnumerable<VoteResponseDto> GetAll();
        VoteResponseDto GetById(int id);
        void Create(VoteRequestDto voteDto);
        void Delete(int id);
    }
}

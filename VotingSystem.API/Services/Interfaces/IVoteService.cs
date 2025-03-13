using VotingSystem.API.DTOs.VoteDtos;

namespace VotingSystem.API.Services.Interfaces
{
    public interface IVoteService
    {
        IEnumerable<VoteResponseDto> GetAll();
        VoteResponseDto GetById(int id);
        VoteResponseDto Create(VoteRequestDto voteDto);
        VoteResponseDto Delete(int id);
    }
}

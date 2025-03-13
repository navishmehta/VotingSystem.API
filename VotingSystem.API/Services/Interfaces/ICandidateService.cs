using VotingSystem.API.DTOs.CandidateDtos;

namespace VotingSystem.API.Services.Interfaces
{
    public interface ICandidateService
    {
        IEnumerable<CandidateResponseDto> GetAll();
        CandidateResponseDto GetById(int id);
        CandidateResponseDto Create(CandidateRequestDto candidateDto);
        CandidateResponseDto Update(int id, CandidateRequestDto candidateDto);
        CandidateResponseDto Delete(int id);
    }
}

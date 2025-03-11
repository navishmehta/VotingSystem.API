using VotingSystem.API.DTOs.CandidateDtos;

namespace VotingSystem.API.Services.Interfaces
{
    public interface ICandidateService
    {
        IEnumerable<CandidateResponseDto> GetAll();
        CandidateResponseDto GetById(int id);
        void Create(CandidateRequestDto candidateDto);
        void Update(int id, CandidateRequestDto candidateDto);
        void Delete(int id);
    }
}

using VotingSystem.API.DTOs.PartyDtos;

namespace VotingSystem.API.Services.Interfaces
{
    public interface IPartyService
    {
        IEnumerable<PartyResponseDto> GetAllParties();
        PartyResponseDto GetById(int id);
        void Create(PartyRequestDto partydto);
        void Update(int id, PartyRequestDto partydto);
        void Delete(int id);
    }
}

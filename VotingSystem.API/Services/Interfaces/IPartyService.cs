using VotingSystem.API.DTOs.PartyDtos;

namespace VotingSystem.API.Services.Interfaces
{
    public interface IPartyService
    {
        IEnumerable<PartyResponseDto> GetAllParties();
        PartyResponseDto GetById(int id);
        PartyResponseDto Create(PartyRequestDto partydto);
        PartyResponseDto Update(int id, PartyRequestDto partydto);
        PartyResponseDto Delete(int id);
    }
}

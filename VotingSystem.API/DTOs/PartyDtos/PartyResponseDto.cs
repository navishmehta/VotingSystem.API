using System.Text.Json.Serialization;

namespace VotingSystem.API.DTOs.PartyDtos
{
    public class PartyResponseDto
    {
        public int Id { get; set; }        
        public string Name { get; set; }        
        public string Symbol { get; set; }
    }
}

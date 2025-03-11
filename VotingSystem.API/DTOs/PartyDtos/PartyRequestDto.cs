using System.Text.Json.Serialization;

namespace VotingSystem.API.DTOs.PartyDtos
{
    public class PartyRequestDto
    {        
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Name { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Symbol { get; set; }
    }
}

using System.Text.Json.Serialization;

namespace VotingSystem.API.DTOs.VoterDtos
{
    public class VoterRequestDto
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Name { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? StateId { get; set; }
    }
}

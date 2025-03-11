using System.Text.Json.Serialization;

namespace VotingSystem.API.DTOs.Voter
{
    public class VoterResponseDto
    {
        public int Id { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Name { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? StateId { get; set; }
    }
}

using System.Text.Json.Serialization;

namespace VotingSystem.API.DTOs.CandidateDtos
{
    public class CandidateRequestDto
    {    
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Name { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? PartyId { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? StateId { get; set; }
    }
}

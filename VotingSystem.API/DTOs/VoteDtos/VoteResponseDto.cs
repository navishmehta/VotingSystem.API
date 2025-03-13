using System.Text.Json.Serialization;

namespace VotingSystem.API.DTOs.VoteDtos
{
    public class VoteResponseDto
    {
        public int Id { get; set; }        
        public int VoterId { get; set; }        
        public int CandidateId { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? CandidateName { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? PartyName { get; set; }
    }
}

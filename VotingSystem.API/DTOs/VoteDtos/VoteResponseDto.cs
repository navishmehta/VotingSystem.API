using System.Text.Json.Serialization;

namespace VotingSystem.API.DTOs.VoteDtos
{
    public class VoteResponseDto
    {
        public int Id { get; set; }        
        public int VoterId { get; set; }        
        public int CandidateId { get; set; }
    }
}

using System.Text.Json.Serialization;

namespace VotingSystem.API.DTOs.VoteDtos
{
    public class VoteRequestDto
    {               
        public int VoterId { get; set; }        
        public int CandidateId { get; set; }
    }
}

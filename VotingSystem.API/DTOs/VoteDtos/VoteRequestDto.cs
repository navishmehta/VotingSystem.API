using System.Text.Json.Serialization;

namespace VotingSystem.API.DTOs.VoteDtos
{
    public class VoteRequestDto
    {
        public int VoterId { get; set; }
        public string CandidateName { get; set; }
        public string PartyName { get; set; }
    }

}

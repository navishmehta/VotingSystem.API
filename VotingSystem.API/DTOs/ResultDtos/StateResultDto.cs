namespace VotingSystem.API.DTOs.ResultDtos
{
    public class StateResultDto
    {
        public int StateId { get; set; }
        public string StateName { get; set; }
        public int PartyId { get; set; }
        public string PartyName { get; set; }
        public int VoteCount { get; set; }
    }
}

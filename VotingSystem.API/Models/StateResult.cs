using System.ComponentModel.DataAnnotations.Schema;

namespace VotingSystem.API.Models
{
    public class StateResult
    {
        public int Id { get; set; }

        [ForeignKey("State")]
        public int StateId { get; set; }
        public State State { get; set; }

        [ForeignKey("Candidate")]
        public int CandidateId { get; set; }
        public Candidate Candidate { get; set; }

        public int VoteCount { get; set; }

    }
}

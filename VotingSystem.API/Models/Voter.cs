using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VotingSystem.API.Models
{
    public class Voter
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [ForeignKey("State")]
        public int StateId { get; set; }
        public State State { get; set; }
    }
}

using System.ComponentModel.DataAnnotations.Schema;

namespace VotingSystem.API.Models
{
    public class NationalResult
    {
        public int Id { get; set; }

        [ForeignKey("Party")]
        public int PartyId { get; set; }
        public Party Party { get; set; }

        public int StatesWon { get; set; }
    }
}

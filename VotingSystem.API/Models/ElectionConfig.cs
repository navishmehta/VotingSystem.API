using System.ComponentModel.DataAnnotations;

namespace VotingSystem.API.Models
{
    public class ElectionConfig
    {        
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}

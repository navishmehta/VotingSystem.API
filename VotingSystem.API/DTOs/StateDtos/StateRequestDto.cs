using System.ComponentModel.DataAnnotations;

namespace VotingSystem.API.DTOs.StateDtos
{
    public class StateRequestDto
    {       
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "State name must contain only letters.")]
        public string Name { get; set; }
    }
}

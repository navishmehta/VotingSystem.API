using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace VotingSystem.API.DTOs.PartyDtos
{
    public class PartyRequestDto
    {        
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Party name must contain only letters.")]
        public string? Name { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Symbol { get; set; }
    }
}

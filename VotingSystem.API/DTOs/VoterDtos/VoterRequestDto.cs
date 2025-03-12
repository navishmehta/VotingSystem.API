using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace VotingSystem.API.DTOs.VoterDtos
{
    public class VoterRequestDto
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Voter name must contain only letters.")]
        public string? Name { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? StateId { get; set; }
    }
}

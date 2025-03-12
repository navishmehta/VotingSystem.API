namespace VotingSystem.API.DTOs.AdminDtos
{
    public class ResetPasswordRequestDto
    {
        public string ResetToken { get; set; }
        public string NewPassword { get; set; }
    }
}

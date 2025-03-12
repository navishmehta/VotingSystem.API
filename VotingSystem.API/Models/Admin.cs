namespace VotingSystem.API.Models
{
    public class Admin
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string? ResetToken { get; set; }
        public DateTime? ResetTokenExpiry { get; set; }
    }

}

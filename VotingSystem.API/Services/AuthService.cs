using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VotingSystem.API.Data;
using VotingSystem.API.Models;

public class AuthService
{
    private readonly VotingSystemDbContext _context;
    private readonly IConfiguration _config;

    public AuthService(VotingSystemDbContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
    }

    public string Login(string username, string password)
    {
        var admin = _context.Admins.FirstOrDefault(a => a.Username == username);
        if (admin == null || admin.PasswordHash != password) // Simple string comparison
            throw new UnauthorizedAccessException("Invalid credentials.");

        return GenerateJwtToken(admin);
    }


    private string GenerateJwtToken(Admin admin)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, admin.Username),
                new Claim(ClaimTypes.Role, "Admin")
            }),
            Expires = DateTime.UtcNow.AddHours(3),
            SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256),
            Issuer = _config["Jwt:Issuer"],
            Audience = _config["Jwt:Audience"]
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}

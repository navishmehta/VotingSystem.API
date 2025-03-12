using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
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

    // Admin Registration (Only One Admin Allowed)
    public string Register(string username, string password)
    {
        if (_context.Admins.Any())
            throw new Exception("Admin already exists.");

        var admin = new Admin
        {
            Username = username,
            PasswordHash = HashPassword(password) // Hash the password correctly
        };

        _context.Admins.Add(admin);
        _context.SaveChanges();

        return GenerateJwtToken(admin);
    }

    // Admin Login
    public string Login(string username, string password)
    {
        var admin = _context.Admins.FirstOrDefault();
        if (admin == null || !VerifyPassword(password, admin.PasswordHash))
            throw new UnauthorizedAccessException("Invalid credentials.");

        return GenerateJwtToken(admin);
    }

    // Request Password Reset (Generates Reset Token)
    public string RequestPasswordReset()
    {
        var admin = _context.Admins.FirstOrDefault();
        if (admin == null)
            throw new Exception("Admin not found.");

        string resetToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
        admin.ResetToken = resetToken;
        admin.ResetTokenExpiry = DateTime.UtcNow.AddMinutes(30);

        _context.SaveChanges();

        Console.WriteLine($"Password reset token: {resetToken}"); // Simulating email sending

        return resetToken;
    }

    // Reset Password Using Reset Token
    public void ResetPassword(string resetToken, string newPassword)
    {
        var admin = _context.Admins.FirstOrDefault();
        if (admin == null || admin.ResetToken != resetToken || admin.ResetTokenExpiry < DateTime.UtcNow)
            throw new Exception("Invalid or expired token.");

        admin.PasswordHash = HashPassword(newPassword); // Hash the new password before storing
        admin.ResetToken = null;
        admin.ResetTokenExpiry = null;

        _context.SaveChanges();
    }

    // Generate JWT Token
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

    // Secure Password Hashing with HMAC-SHA256
    private string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(password);
        var hash = sha256.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
    }

    // Verify Password Hash
    private bool VerifyPassword(string password, string storedHash)
    {
        return HashPassword(password) == storedHash;
    }
}

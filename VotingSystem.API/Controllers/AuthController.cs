using Microsoft.AspNetCore.Mvc;
using VotingSystem.API.DTOs.AdminDtos;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public IActionResult Register([FromBody] RegisterRequestDto request)
    {
        try
        {
            var token = _authService.Register(request.Username, request.Password);
            return Ok(new { Token = token });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequestDto request)
    {
        try
        {
            var token = _authService.Login(request.Username, request.Password);
            return Ok(new { Token = token });
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { Message = ex.Message });
        }
    }

    [HttpPost("request-password-reset")]
    public IActionResult RequestPasswordReset()
    {
        try
        {
            var resetToken = _authService.RequestPasswordReset();
            return Ok(new { ResetToken = resetToken });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

    [HttpPost("reset-password")]
    public IActionResult ResetPassword([FromBody] ResetPasswordRequestDto request)
    {
        try
        {
            _authService.ResetPassword(request.ResetToken, request.NewPassword);
            return Ok(new { Message = "Password reset successful" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }
}
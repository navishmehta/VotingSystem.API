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
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while registering.", Details = ex.Message });
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
            return Unauthorized(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while logging in.", Details = ex.Message });
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
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while requesting password reset.", Details = ex.Message });
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
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while resetting password.", Details = ex.Message });
        }
    }

    [HttpDelete("delete-account")]
    public IActionResult DeleteAccount()
    {
        try
        {
            _authService.DeleteAccount();
            return Ok(new { Message = "Admin account deleted successfully." });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while deleting the account.", Details = ex.Message });
        }
    }

}

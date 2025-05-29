using JwT_with_RefreshToken.AuthService;
using JwT_with_RefreshToken.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace JwT_with_RefreshToken.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        readonly IAuthService _authService;
        readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] TokenRequest request, CancellationToken cancellationToken)
        {
            var tokenResponse = await _authService.LoginAsync(request, cancellationToken);
            if (tokenResponse is null)
                return Unauthorized(new { message = "Invalid credentials." });

            Response.Cookies.Append("refreshToken", tokenResponse.RefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
            });

            return Ok( new { tokenResponse.AccessToken });
        }
        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken(CancellationToken cancellationToken)
        {
            var refreshToken = Request.Cookies["refreshToken"];
            if (string.IsNullOrEmpty(refreshToken))
                return Unauthorized();

            var tokenResponse = await _authService.RefreshTokenAsync(refreshToken, cancellationToken);

            if (tokenResponse is null)
                return Unauthorized(new { message = "Invalid refresh token." });

            Response.Cookies.Append("refreshToken", tokenResponse.RefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
            });

            return Ok(new { tokenResponse.AccessToken });
        }
        [HttpPost("create-user")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request, CancellationToken cancellationToken)
        {
            var result = await _authService.CreateUserAsync(request, cancellationToken);

            if (!result.Success)
            {
                if (result.ErrorCode == "UserExists")
                    return Conflict(new { message = "User with this email already exists." });

                return BadRequest(new { message = result.ErrorMessage });
            }
            Response.Cookies.Append("refreshToken", result.TokenResponse.RefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
            });
            return Ok(result.TokenResponse.AccessToken);
        }
    }
}

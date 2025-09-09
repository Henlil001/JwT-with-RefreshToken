using JwT_with_RefreshToken.AuthService;
using JwT_with_RefreshToken.Common;
using JwT_with_RefreshToken.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Validations;
using System.Reflection.Metadata.Ecma335;
using System.Threading;

namespace JwT_with_RefreshToken.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        readonly IAuthenticationService _authService;

        readonly string cookieNameRefreshToken;


        public AuthController(IAuthenticationService authService, IOptions<AppSettings> appsettings)
        {
            _authService = authService;
            cookieNameRefreshToken = appsettings.Value.RefreshTokenCookieName;
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
        {
            var tokenResponse = await _authService.LoginAsync(request, cancellationToken);
            if (tokenResponse is null)
                return Unauthorized(new { message = "Invalid credentials." });

            Response.Cookies.Append(cookieNameRefreshToken, tokenResponse.RefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
            });

            return Ok(new { tokenResponse.AccessToken });
        }
        [Authorize]
        [HttpPost("logout")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> LogOut(CancellationToken cancellationToken)
        {
            var refreshToken = Request.Cookies[cookieNameRefreshToken];
            if (!string.IsNullOrEmpty(refreshToken))
            {
              var result = await _authService.RevokeRefreshToken(refreshToken, cancellationToken);
                if (!result)
                {
                    Response.Cookies.Append(cookieNameRefreshToken, "", new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.None,
                        Expires = DateTime.UtcNow.AddDays(-1)
                    });
                    return Ok(new { message = "Logged out" });
                }
            }

            return BadRequest();
        }

        [HttpPost("refresh")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> RefreshToken(CancellationToken cancellationToken)
        {
            var refreshToken = Request.Cookies[cookieNameRefreshToken];
            if (string.IsNullOrEmpty(refreshToken))
                return Unauthorized();

            var tokenResponse = await _authService.RefreshTokenAsync(refreshToken, cancellationToken);

            if (tokenResponse is null)
                return Unauthorized(new { message = "Invalid refresh token." });

            Response.Cookies.Append(cookieNameRefreshToken, tokenResponse.RefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
            });

            return Ok(new { tokenResponse.AccessToken });
        }
        [HttpPost("create-user")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request, CancellationToken cancellationToken)
        {
            var result = await _authService.CreateUserAsync(request, cancellationToken);

            if (!result.Success)
            {
                if (result.ErrorCode == "UserExists")
                    return Conflict(new { message = "User with this email already exists." });
                else
                {
                    return Unauthorized();
                }

            }
            Response.Cookies.Append(cookieNameRefreshToken, result.TokenResponse.RefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
            });
            return Ok(new { result.TokenResponse.AccessToken });
        }
    }
}

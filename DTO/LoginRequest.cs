using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace JwT_with_RefreshToken.DTO
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email.")]
        [JsonPropertyName("email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}

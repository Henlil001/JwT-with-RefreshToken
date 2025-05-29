namespace JwT_with_RefreshToken.DTO
{
    public class CreateUserResult
    {
        public bool Success { get; set; }
        public string ErrorCode { get; set; } // e.g., "UserExists"
        public string ErrorMessage { get; set; }
        public TokenResponse TokenResponse { get; set; }
    }
}

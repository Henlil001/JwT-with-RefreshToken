namespace JwT_with_RefreshToken.DTO
{
    public class CreateUserResult
    {
        public bool Success { get; set; }
        public string ErrorCode { get; set; } // "UserExists"
        public TokenResponse TokenResponse { get; set; } = new TokenResponse();
    }
}

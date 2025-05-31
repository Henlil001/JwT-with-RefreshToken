namespace JwT_with_RefreshToken.Common
{
    public class AppSettings
    {
        public string DbConnString { get; set; }
        public string ClientDomain { get; set; }
        public string CorsPolicyName { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string TokenKey { get; set; }
        public Roles Roles { get; set; }
    }

    public class Roles
    {
        public string User { get; set; }
        public string Admin { get; set; }
    }
}

namespace Domain.Common
{
    public class JwtConfig
    {
        public string JwtKey { get; set; }
        public int JwtExpires { get; set; }
        public string JwtIssuer { get; set; }
        public string JwtAudience { get; set; }
    }
}
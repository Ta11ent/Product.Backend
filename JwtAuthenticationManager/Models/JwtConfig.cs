namespace JwtAuthenticationManager.Models
{
    public class JwtConfig
    {
        public string Secret { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public int ExpAccToken { get; set; }
        public int ExpRefToken { get; set; }
    }
}

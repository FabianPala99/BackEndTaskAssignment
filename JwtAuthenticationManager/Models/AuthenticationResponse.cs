namespace JwtAuthenticationManager.Models
{
    public class AuthenticationResponse
    {
        public int? UserId { get; set; }
        public string? Name { get; set; }
        public string? RoleName { get; set; }        
        public string? JwtToken { get; set; }
        public int ExpiresIn { get; set; }
    }
}

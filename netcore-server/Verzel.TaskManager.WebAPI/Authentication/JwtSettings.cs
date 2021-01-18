namespace Verzel.TaskManager.WebAPI.Authentication
{
    public class JwtSettings
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public long DurationInMinutes { get; set; }
        public string SignInKey { get; set; }
    }
}

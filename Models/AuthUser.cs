namespace rapid_news_media_auth_api.Models
{
    public class AuthUser
    {

        public long Id { get; set;}
        public string? Username { get; set;}

        public string? Password { get; set;}

        public string? AuthToken { get; set;}
    }
}
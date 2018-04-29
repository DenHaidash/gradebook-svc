namespace GradeBook.Options
{
    public class JwtOptions
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public int TokenLifetimeMinutes { get; set; }
    }
}
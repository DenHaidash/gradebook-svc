namespace GradeBook.Common.Mailing
{
    public class EmailSenderSettings
    {
        public string FromAddress { get; set; }
        public string FromName { get; set; }
        public string SmptAccountLogin { get; set; }
        public string SmptAccountPassword { get; set; }
        public string SmptServerUrl { get; set; }
        public int SmptServerPort { get; set; }
    }
}
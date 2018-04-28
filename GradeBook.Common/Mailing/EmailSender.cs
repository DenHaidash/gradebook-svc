using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace GradeBook.Common.Mailing
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailSenderSettings _settings;

        public EmailSender(IOptions<EmailSenderSettings> settings)
        {
            _settings = settings.Value;
        }

        public async Task SendEmailAsync(string toAddress, string subject, string message)
        {
            var sender = new MailAddress(_settings.FromAddress, _settings.FromName);
            var receiver = new MailAddress(toAddress, String.Empty);

            var smtp = new SmtpClient
            {
                Host = _settings.SmptServerUrl,
                Port = _settings.SmptServerPort,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_settings.SmptAccountLogin, _settings.SmptAccountPassword)
            };
            
            using (var mail = new MailMessage(sender, receiver)
            {
                Subject = subject,
                Body = message
            })
            {
                await smtp.SendMailAsync(mail);
            }
        }
    }
}

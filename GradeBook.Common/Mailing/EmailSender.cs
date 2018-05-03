using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using GradeBook.Common.Options;
using Microsoft.Extensions.Options;

namespace GradeBook.Common.Mailing
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailSenderOptions _options;

        public EmailSender(IOptions<EmailSenderOptions> settings)
        {
            _options = settings.Value;
        }

        public async Task SendEmailAsync(string toAddress, string subject, string message)
        {
            var sender = new MailAddress(_options.FromAddress, _options.FromName);
            var receiver = new MailAddress(toAddress, string.Empty);

            var smtp = new SmtpClient
            {
                Host = _options.SmptServerUrl,
                Port = _options.SmptServerPort,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_options.SmptAccountLogin, _options.SmptAccountPassword)
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

using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using GradeBook.Common.Mailing.Abstractions;
using GradeBook.Common.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace GradeBook.Common.Mailing
{
    public class EmailSender : IEmailSender
    {
        private readonly ILogger<EmailSender> _logger;
        private readonly EmailSenderOptions _options;

        public EmailSender(IOptions<EmailSenderOptions> settings, ILogger<EmailSender> logger)
        {
            _logger = logger;
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
                try
                {
                    await smtp.SendMailAsync(mail);
                }
                catch (Exception ex)
                {
                    _logger.LogError("Email sending failed", ex);
                }
            }
        }
    }
}

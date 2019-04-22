using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Courses.Application.Interfaces;
using Courses.Domain.Configurations;
using Microsoft.Extensions.Options;

namespace Courses.Infrastructure
{
    public class EmailService : INotificationService
    {
        private readonly IOptions<SmtpConfiguration> _smtpConfiguration;

        public EmailService(IOptions<SmtpConfiguration> smtpConfiguration)
        {
            _smtpConfiguration = smtpConfiguration;
        }

        public async Task SendEmailAsync(MailMessage message)
        {
            var smtpClient = new SmtpClient
            {
                Host =  _smtpConfiguration.Value.Host,
                Port = _smtpConfiguration.Value.Port,
                Credentials = new NetworkCredential(_smtpConfiguration.Value.User, _smtpConfiguration.Value.Password),
                EnableSsl = true
            };

            await smtpClient.SendMailAsync(message);
        }
    }
}

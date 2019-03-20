using System.Net.Mail;
using System.Threading.Tasks;

namespace Courses.Infrastructure
{
    public interface IEmailService
    {
        Task SendEmailAsync(MailMessage message);
    }
}

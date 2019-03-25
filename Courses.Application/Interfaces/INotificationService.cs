using System.Net.Mail;
using System.Threading.Tasks;

namespace Courses.Application.Interfaces
{
    public interface INotificationService
    {
        Task SendEmailAsync(MailMessage message);
    }
}

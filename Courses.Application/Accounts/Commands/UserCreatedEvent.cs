using System;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using Courses.Application.Interfaces;
using Courses.Emails;
using Courses.Emails.Views;
using MediatR;

namespace Courses.Application.Accounts.Commands
{
    public class UserCreatedEvent : INotification
    {
        public string Token { get; set; }

        public string Username { get; set; }

        public Guid UserId { get; set; }

        public string Email { get; set; }
    }

    public class UserCreatedEventHandler : INotificationHandler<UserCreatedEvent>
    {
        private readonly INotificationService _notification;
        private readonly IRazorViewToStringRenderer _razorViewToStringRenderer;

        public UserCreatedEventHandler(INotificationService notification, IRazorViewToStringRenderer razorViewToStringRenderer)
        {
            _notification = notification;
            _razorViewToStringRenderer = razorViewToStringRenderer;
        }

        public async Task Handle(UserCreatedEvent @event, CancellationToken cancellationToken)
        {
            var confirmAccountModel = new ConfirmAccountEmailViewModel
            {
                Token = @event.Token,
                Username = @event.Username,
                UserId = @event.UserId
            };

            string body = await _razorViewToStringRenderer.RenderViewToStringAsync("/Views/ConfirmAccountEmail.cshtml", confirmAccountModel);

            await _notification.SendEmailAsync(new MailMessage
            {
                Body = body,
                From = new MailAddress("admin@courses.by"),
                To = { new MailAddress(@event.Email) },
                Priority = MailPriority.High,
                Subject = $"Account confirmation for user {@event.Username}",
                IsBodyHtml = true
            });
        }
    }
}
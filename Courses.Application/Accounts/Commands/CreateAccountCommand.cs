using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using Courses.Domain.Entities;
using Courses.Emails;
using Courses.Emails.Views;
using Courses.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Courses.Application.Accounts.Commands
{
    public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, Unit>
    {
        private readonly UserManager<User> _userManager;
        private readonly IEmailService _emailService;
        private readonly IRazorViewToStringRenderer _razorViewToStringRenderer;

        public CreateAccountCommandHandler(UserManager<User> userManager, IEmailService emailService, IRazorViewToStringRenderer razorViewToStringRenderer)
        {
            _userManager = userManager;
            _emailService = emailService;
            _razorViewToStringRenderer = razorViewToStringRenderer;
        }

        public async Task<Unit> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            var appUser = new User
            {
                Email = request.Email, 
                UserName = request.Username,
                FirstName = request.Firstname,
                LastName = request.Lastname
            };

            await _userManager.CreateAsync(appUser, request.Password);

           

            var confirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(appUser);


            var a = new ConfirmAccountEmailModel
            {
                Token = confirmationToken
            };

            string body = await _razorViewToStringRenderer.RenderViewToStringAsync("/Views/ConfirmAccountEmail.cshtml", a);

            await _emailService.SendEmailAsync(new MailMessage
            {
                Body = body,
                From = new MailAddress("admin@courses.by"),
                To = { new MailAddress(appUser.Email)},
                Priority = MailPriority.High,
                Subject = $"Account confirmation for user {appUser.UserName}"
            });

            return Unit.Value;
        }
    }

    public class CreateAccountCommand : IRequest
    {
        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Email { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
    }
}

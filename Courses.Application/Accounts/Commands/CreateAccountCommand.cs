using System.Threading;
using System.Threading.Tasks;
using Courses.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Courses.Application.Accounts.Commands
{
    public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, Unit>
    {
        private readonly UserManager<User> _userManager;
        private readonly IMediator _mediator;

        public CreateAccountCommandHandler(UserManager<User> userManager, IMediator mediator)
        {
            _userManager = userManager;
            _mediator = mediator;
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

            await _mediator.Publish(new UserCreatedEvent
            {
                Token = await _userManager.GenerateEmailConfirmationTokenAsync(appUser),
                Username = appUser.UserName,
                UserId = appUser.Id,
                Email = appUser.Email
            }, cancellationToken);

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

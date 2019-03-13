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

        public CreateAccountCommandHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Unit> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            var appUser = new User {Email = request.Email, UserName = request.Username};
            await _userManager.CreateAsync(appUser, request.Password);

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

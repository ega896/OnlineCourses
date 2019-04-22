using System;
using System.Threading;
using System.Threading.Tasks;
using Courses.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Courses.Application.Accounts.Commands
{
    public class ConfirmAccountEmailCommand : IRequest
    {
        public string Token { get; set; }

        public Guid UserId { get; set; }
    }

    public class ConfirmAccountEmailCommandHandler : IRequestHandler<ConfirmAccountEmailCommand, Unit>
    {
        private readonly UserManager<User> _userManager;

        public ConfirmAccountEmailCommandHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Unit> Handle(ConfirmAccountEmailCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());

            await _userManager.ConfirmEmailAsync(user, request.Token);

            return Unit.Value;
        }
    }
}

using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Courses.Domain.Entities;
using Courses.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Courses.Application.Auth.Queries
{
    public class GetTokenQueryHandler : IRequestHandler<GetTokenQuery, object>
    {
        private readonly UserManager<User> _userManager;
        private readonly IJwtFactory _jwtFactory;
        private readonly JwtIssuerOptions _jwtOptions;

        public GetTokenQueryHandler(UserManager<User> userManager, IJwtFactory jwtFactory, JwtIssuerOptions jwtOptions)
        {
            _userManager = userManager;
            _jwtFactory = jwtFactory;
            _jwtOptions = jwtOptions;
        }

        public async Task<object> Handle(GetTokenQuery request, CancellationToken cancellationToken)
        {
            var identity = await GetClaimsIdentity(request.Username, request.Password);

            return new
            {
                auth_token = await _jwtFactory.GenerateEncodedToken(identity),
                expires_in = (int) _jwtOptions.ValidFor.TotalSeconds
            };
        }

        private async Task<ClaimsIdentity> GetClaimsIdentity(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                return await Task.FromResult<ClaimsIdentity>(null);

            var userToVerify = await _userManager.FindByNameAsync(userName);

            if (userToVerify == null) return await Task.FromResult<ClaimsIdentity>(null);

            if (await _userManager.CheckPasswordAsync(userToVerify, password))
            {
                return await Task.FromResult(_jwtFactory.GenerateClaimsIdentity(userName, userToVerify.Id.ToString()));
            }

            return await Task.FromResult<ClaimsIdentity>(null);
        }
    }

    public class GetTokenQuery : IRequest<object>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}

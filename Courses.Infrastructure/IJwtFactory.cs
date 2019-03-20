using System.Security.Claims;
using System.Threading.Tasks;

namespace Courses.Infrastructure
{
    public interface IJwtFactory
    {
        Task<string> GenerateEncodedToken(ClaimsIdentity identity);

        ClaimsIdentity GenerateClaimsIdentity(string userName, string id);
    }
}
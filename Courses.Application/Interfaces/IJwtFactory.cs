using System.Security.Claims;
using System.Threading.Tasks;

namespace Courses.Application.Interfaces
{
    public interface IJwtFactory
    {
        Task<string> GenerateEncodedToken(ClaimsIdentity identity);

        ClaimsIdentity GenerateClaimsIdentity(string userName, string id);
    }
}
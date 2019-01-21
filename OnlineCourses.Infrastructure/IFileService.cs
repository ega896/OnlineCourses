using System.Collections.Generic;
using System.Threading;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Courses.Infrastructure
{
    public interface IFileService
    {
        Task SaveFiles(ICollection<IFormFile> files, CancellationToken cancellationToken);
    }
}

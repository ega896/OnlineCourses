using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Courses.Infrastructure
{
    public interface IFileService
    {
        Task<long> SaveFiles(ICollection<IFormFile> files);

        Task<byte[]> GetFile(string path, string fileName);

        Task<string> SaveFile(IFormFile file);
    }
}

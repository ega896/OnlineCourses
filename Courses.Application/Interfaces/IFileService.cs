using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Courses.Application.Interfaces
{
    public interface IFileService
    {
        Task<long> SaveFiles(ICollection<IFormFile> files);

        Task<byte[]> GetFile(string path, string fileName);

        Task<string> SaveFile(IFormFile file);
    }
}

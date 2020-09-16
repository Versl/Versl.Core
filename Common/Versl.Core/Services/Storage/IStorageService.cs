using System;
using System.IO;
using System.Threading.Tasks;

namespace Versl.Services.Storage
{
    public interface IStorageService
    {
        Task<string> PutAsync(string folder, string filename, Stream stream);
        Task DeleteAsync(string folder, string filename);
        Task<string> GetDownloadUrlAsync(string folder, string filename);
    }
}

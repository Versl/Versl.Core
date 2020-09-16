using System.IO;
using System.Threading.Tasks;
using Firebase.Storage;
using MvvmCross;
using MvvmCross.Plugin.Messenger;
using Versl.Services.Storage;

namespace Versl.Firebase.Storage
{
    public class StorageService : IStorageService
    {
        FirebaseStorage _provider;
        
        public StorageService() 
        {
            var config = Mvx.IoCProvider.Resolve<IFirebaseConfig>();

            _provider = new FirebaseStorage(config.StorageBucket);        
        }

        public async Task DeleteAsync(string folder, string filename)
        {
            await _provider.Child(folder).Child(filename).DeleteAsync();            
        }

        public async Task<string> GetDownloadUrlAsync(string folder, string filename)
        {
            return await _provider.Child(folder).Child(filename).GetDownloadUrlAsync();
        }

        public async Task<string> PutAsync(string folder, string filename, Stream stream)
        {
            return await _provider.Child(folder).Child(filename).PutAsync(stream);
        }
    }
}

using System;
using Versl.Services;
using Firebase.Database;
using MvvmCross;

namespace Versl.Firebase.Data
{    
    public class DatabaseClientProvider : ServiceBase<DatabaseClientProvider>
    {
        private static FirebaseClient _firbaseClientInstance;

        private IFirebaseConfig _config;

        public DatabaseClientProvider()
        {
            _config = Mvx.IoCProvider.Resolve<IFirebaseConfig>();
        }

        public FirebaseClient GetClient()
        {
			if (_firbaseClientInstance == null)
			{
				_firbaseClientInstance = new FirebaseClient(_config.DatabaseURL);
			}

			return _firbaseClientInstance;
		}
	}
}

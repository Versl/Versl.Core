using Foundation;
using Versl.Services;
using Versl.Firebase;

namespace Versl.iOS.Utility
{
    public class FirebaseConfigurationFileReader : ServiceBase<FirebaseConfigurationFileReader>, IFirebaseConfigurationReader
    {
        public IFirebaseConfig GetFirebaseConfiguration()
        {
            var config = new FirebaseConfig();

            var path = NSBundle.MainBundle.PathForResource("GoogleService-Info", "plist");
            var data = NSDictionary.FromFile(path);

            config.APIKey = data["API_KEY"].ToString();
            config.AuthDomain = data["STORAGE_BUCKET"].ToString();
            config.DatabaseURL = data["DATABASE_URL"].ToString();
            config.ProjectID = data["PROJECT_ID"].ToString();
            config.StorageBucket = data["STORAGE_BUCKET"].ToString();
            config.MessagingSenderID = data["GCM_SENDER_ID"].ToString();
            config.AppID = data["GOOGLE_APP_ID"].ToString();

            return config;
        }
    }
}

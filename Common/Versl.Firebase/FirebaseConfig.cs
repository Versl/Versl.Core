using System;
namespace Versl.Firebase
{
    public class FirebaseConfig : IFirebaseConfig
    {        
        public string APIKey { get; set; }
        public string AuthDomain { get; set; }
        public string DatabaseURL { get; set; }
        public string ProjectID { get; set; }
        public string StorageBucket { get; set; }
        public string MessagingSenderID { get; set; }
        public string AppID { get; set; }
        public string MeasurementID { get; set; }
    }
}

using System;
namespace Versl.Firebase
{
    public interface IFirebaseConfig
    {
        string APIKey { get; set; }
        string AuthDomain { get; set; }
        string DatabaseURL { get; set; }
        string ProjectID { get; set; }
        string StorageBucket { get; set; }
        string MessagingSenderID { get; set; }
        string AppID { get; set; }
        string MeasurementID { get; set; }
    }
}

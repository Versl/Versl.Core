using System.Collections.Generic;
using Android.OS;
using Firebase.Analytics;
using Versl.Droid.Analytics;
using Versl.Services.Analytics;
using Xamarin.Forms;

[assembly: Dependency(typeof(AnalyticsService))]
namespace Versl.Droid.Analytics
{

    public class AnalyticsService : IAnalyticsService
    {
        public void SendEvent(string eventId)
        {
            SendEvent(eventId, null);
        }

        public void SendEvent(string eventId, string paramName, string value)
        {
            SendEvent(eventId, new Dictionary<string, string>
            {
                {paramName, value}
            });
        }

        public void SendEvent(string eventId, IDictionary<string, string> parameters)
        {
            var firebaseAnalytics = FirebaseAnalytics.GetInstance(Forms.Context);

            if (parameters == null)
            {
                firebaseAnalytics.LogEvent(eventId, null);
                return;
            }

            var bundle = new Bundle();
            foreach (var param in parameters)
            {
                bundle.PutString(param.Key, param.Value);
            }

            firebaseAnalytics.LogEvent(eventId, bundle);
        }
    }
}
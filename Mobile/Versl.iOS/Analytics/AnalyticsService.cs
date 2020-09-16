using System.Collections.Generic;
using Foundation;
using Versl.iOS.Analytics;
using Versl.Services.Analytics;
using Xamarin.Forms;
using FB = Firebase;

[assembly: Dependency(typeof(AnalyticsService))]
namespace Versl.iOS.Analytics
{
    public class AnalyticsService : IAnalyticsService
    {
        public void SendEvent(string eventId)
        {
            SendEvent(eventId, (IDictionary<string, string>)null);
        }

        public void SendEvent(string eventId, string paramName, string value)
        {
            SendEvent(eventId, new Dictionary<string, string>
            {
                { paramName, value }
            });
        }

        public void SendEvent(string eventId, IDictionary<string, string> parameters)
        {
            if (parameters == null)
            {
                FB.Analytics.Analytics.LogEvent(eventId, (Dictionary<object, object>)null);
                return;
            }

            var keys = new List<NSString>();
            var values = new List<NSString>();
            foreach (var item in parameters)
            {
                keys.Add(new NSString(item.Key));
                values.Add(new NSString(item.Value));
            }

            var parametersDictionary =
                NSDictionary<NSString, NSObject>.FromObjectsAndKeys(values.ToArray(), keys.ToArray(), keys.Count);
            FB.Analytics.Analytics.LogEvent(eventId, parametersDictionary);
        }
    }
}
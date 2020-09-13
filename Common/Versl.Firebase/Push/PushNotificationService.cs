using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Versl.Models;
using Versl.Services.Push;

namespace Versl.Firebase.Push
{
    public class PushNotificationService : IPushNotificationService
    {
        private readonly IFirebaseConfig _firebaseConfig;

        public PushNotificationService(IFirebaseConfig firebaseConfig)
        {
            _firebaseConfig = firebaseConfig;
        }

        public async Task SendAsync(PushNotificationMessage message)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"https://us-central1-{_firebaseConfig.ProjectID}.cloudfunctions.net/sendPushNotification");
            request.Content = new StringContent(JsonConvert.SerializeObject(message), Encoding.UTF8, "application/json");

            HttpResponseMessage result;
            using (var client = new HttpClient())
            {
                result = await client.SendAsync(request);
            }
        }
    }
}

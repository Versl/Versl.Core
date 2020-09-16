using System;
using System.Collections.Generic;
namespace Versl.Services.Analytics
{
    public interface IAnalyticsService
    {
        void SendEvent(string eventId);
        void SendEvent(string eventId, string paramName, string value);
        void SendEvent(string eventId, IDictionary<string, string> parameters);
    }
}
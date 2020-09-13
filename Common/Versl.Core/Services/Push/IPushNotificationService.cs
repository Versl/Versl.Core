using System;
using System.Threading.Tasks;
using Versl.Models;

namespace Versl.Services.Push
{
    public interface IPushNotificationService
    {
        Task SendAsync(PushNotificationMessage message);
    }
}

using System.Windows.Input;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.Plugin.Messenger;
using Versl.Services;
using Versl.Models;
using Versl.Services.Data;
using Versl.Services.Media;
using System.Threading.Tasks;
using System;
using System.Net.Http;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Text;
using Versl.Services.Analytics;
using Versl.Services.Push;

namespace Versl.ViewModels.Admin
{
    public class AdminPushNotificationsViewModel : AdminViewModelBase, IAdminPushNotificationsViewModel
    {
        private string _title;
        private string _body;

        private readonly IPushNotificationService _pushService;

        public AdminPushNotificationsViewModel(IPushNotificationService pushService, IMvxMessenger messenger, IContentDataService contentService, IMvxNavigationService navigationService, IUserInteractionService userInteractionService, IMediaService mediaService, IAnalyticsService analyticsService) : base(messenger, navigationService, userInteractionService, mediaService, analyticsService)
        {
            _pushService = pushService;
        }

        public override string PageTitle { get => "Push Notifications"; }

        public string Title
        {
            get;set;
        }

        public string Body
        {
            get;set;
        }

        private bool Validate()
        {
            if (string.IsNullOrEmpty(Title))
                return false;

            if (string.IsNullOrEmpty(Body))
                return false;

            return true;
        }
        public ICommand SendCommand
        {
            get
            {
                return new MvxAsyncCommand<ContentItem>(async (ContentItem arg) => {
                    if (!Validate())
                    {
                        await UserInteractionService.DisplayAlert("Error", "Missing required fields", "Ok");
                        return;
                    }

                    await SendAsync();

                    await UserInteractionService.DisplayAlert("Send", "Your push notification was sent.", "Ok");
                    await Close();
                });
            }
        }

        private async Task SendAsync()
        {
            var push = new PushNotificationMessage();
            push.Title = Title;
            push.Body = Body;

            await _pushService.SendAsync(push);
        }
    }
}

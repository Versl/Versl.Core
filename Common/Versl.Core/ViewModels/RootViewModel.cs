using System;
using System.Threading.Tasks;
using MvvmCross;
using MvvmCross.Navigation;
using MvvmCross.Plugin.Messenger;
using Plugin.FirebasePushNotification;
using Versl.Services;
using Versl.Services.Analytics;
using Versl.Services.Auth;
using Versl.Services.Data;
using Versl.Services.Media;
using Versl.Utility;
using Xamarin.Essentials;

namespace Versl.ViewModels
{
    public interface IMainViewModel : IViewModelBase
    {
    }

    public class RootViewModel : ViewModelBase, IMainViewModel
    {
        private readonly IAuthenticationService _authenticationService;
        private bool _hasAppeared = false;
        
        public RootViewModel(IAuthenticationService authenticationService, IMvxMessenger messenger, IMvxNavigationService navigationService, IUserInteractionService userInteractionService, IMediaService mediaService, IAnalyticsService analyticsService) : base(messenger, navigationService, userInteractionService, mediaService, analyticsService)
        {
            _authenticationService = authenticationService;
        }

        public override async Task Initialize()
        {
            await base.Initialize();

            if (!string.IsNullOrEmpty(Settings.RefreshToken))
            {
                await _authenticationService.RefreshLoginAsync(Settings.RefreshToken);                
            }

            CrossFirebasePushNotification.Current.Subscribe("general");
            CrossFirebasePushNotification.Current.OnNotificationReceived += Current_OnNotificationReceived;
        }

        private void Current_OnNotificationReceived(object source, FirebasePushNotificationDataEventArgs e)
        {
            var title = e.Data.ContainsKey("aps.alert.title") ? e.Data["aps.alert.title"].ToString() : e.Data["title"].ToString();
            var message = e.Data.ContainsKey("aps.alert.body") ? e.Data["aps.alert.body"].ToString() : e.Data["body"].ToString();

            MainThread.BeginInvokeOnMainThread(async () =>
            {                
                await UserInteractionService.DisplayAlert(title, message, "Ok");
            });
        }

        public async override void ViewAppearing()
        {
            base.ViewAppearing();

            if (_hasAppeared == false)
            {
                await ShowMasterViewModel();

                await ShowDetailViewModel();
                _hasAppeared = true;
            }
        }

        private async Task ShowMasterViewModel()
        {
            await Navigate< IMainMasterDetailMasterViewModel>();
        }

        private async Task ShowDetailViewModel()
        {
            await Navigate<IMainMasterDetailDetailViewModel>();
        }
    }
}
using System;
using MvvmCross.Navigation;
using MvvmCross.Plugin.Messenger;
using Versl.Models;
using Versl.Services;
using Versl.Services.Analytics;
using Versl.Services.Auth;
using Versl.Services.Data;
using Versl.Services.Media;
using Xamarin.Forms;

namespace Versl.ViewModels
{
    public class HorizontalVideoPlayerViewModel : ViewModelBase, IHorizontalVideoPlayerViewModel
    {
        public HorizontalVideoPlayerViewModel(IAuthenticationService authenticationService, IMvxMessenger messenger, IContentDataService contentService, IMvxNavigationService navigationService, IUserInteractionService userInteractionService, IMediaService mediaService, IAnalyticsService analyticsService) : base(messenger, navigationService, userInteractionService, mediaService, analyticsService)
        {
        }

        public override void ViewAppearing()
        {
            base.ViewAppearing();

            MessagingCenter.Send<HorizontalVideoPlayerViewModel>(this, "AllowLandscape");
        }

        public override void ViewDisappearing()
        {
            base.ViewDisappearing();

            MessagingCenter.Send<HorizontalVideoPlayerViewModel>(this, "AllowPortrait");
        }
    }
}

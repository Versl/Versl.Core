using System;
using MvvmCross.Navigation;
using MvvmCross.Plugin.Messenger;
using Versl.Services;
using Versl.Services.Analytics;
using Versl.Services.Auth;
using Versl.Services.Data;
using Versl.Services.Media;

namespace Versl.ViewModels
{
    public class TabbedContentDetailViewModel : ContentDetailViewModel, ITabbedContentDetailViewModel
    {
        public TabbedContentDetailViewModel(IMvxMessenger messenger, IAuthenticationService authenticationService, IContentDataService contentService, IMvxNavigationService navigationService, IUserInteractionService userInteractionService, IMediaService mediaService, IAnalyticsService analyticsService) : base(messenger, authenticationService, contentService, navigationService, userInteractionService, mediaService, analyticsService)
        {
        }
    }
}

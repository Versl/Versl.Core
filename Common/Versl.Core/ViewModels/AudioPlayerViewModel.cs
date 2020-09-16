﻿using MvvmCross.Navigation;
using MvvmCross.Plugin.Messenger;
using Versl.Services;
using Versl.Services.Analytics;
using Versl.Services.Auth;
using Versl.Services.Data;
using Versl.Services.Media;

namespace Versl.ViewModels
{
    public class AudioPlayerViewModel : ViewModelBase, IAudioPlayerViewModel
    {
        public AudioPlayerViewModel(IAuthenticationService authenticationService, IMvxMessenger messenger, IContentDataService contentService, IMvxNavigationService navigationService, IUserInteractionService userInteractionService, IMediaService mediaService, IAnalyticsService analyticsService) : base(messenger, navigationService, userInteractionService, mediaService, analyticsService)
        {
        }
    }
}

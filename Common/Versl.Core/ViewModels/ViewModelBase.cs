using System.Threading.Tasks;
using System.Windows.Input;
using MediaManager.Player;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Forms.Views;
using MvvmCross.Navigation;
using MvvmCross.Plugin.Messenger;
using MvvmCross.ViewModels;
using Versl.Enums;
using Versl.Services;
using Versl.Services.Analytics;
using Versl.Services.Auth;
using Versl.Services.Media;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Versl.ViewModels
{
    public abstract class ViewModelBase : MvxViewModel<object>, IViewModelBase
    {
        private string _pageTitle;
        private bool _isBusy;
        private bool _isMediaLoaded;
        private bool _isMediaPlaying;
        private bool _isMediaPaused;
        private MediaAsset _mediaAsset;
        private bool _isLoggedIn;
        private ImageSource _pageIconImageSource;

        private readonly MvxSubscriptionToken _authenticationToken;
        private readonly MvxSubscriptionToken _mediaToken;

        private readonly IMvxNavigationService NavigationService;
        protected readonly IUserInteractionService UserInteractionService;
        protected readonly IMediaService MediaService;
        protected readonly IAnalyticsService AnalyticsService;

        public ViewModelBase(IMvxMessenger messenger, IMvxNavigationService navigationService, IUserInteractionService userInteractionService, IMediaService mediaService, IAnalyticsService analyticsService)
        {
            NavigationService = navigationService;
            UserInteractionService = userInteractionService;
            MediaService = mediaService;
            AnalyticsService = analyticsService;

            _authenticationToken = messenger.Subscribe<AuthenticationMessage>(HandleAuthentication);
            _mediaToken = messenger.Subscribe<MediaMessage>(HandleMedia);
        }

        public object NavigationParameter { get; protected set; }

        private void HandleAuthentication(AuthenticationMessage message)
        {
            IsUserLoggedIn = message.CurrentUser != null;
        }

        private void HandleMedia(MediaMessage message)
        {
            IsMediaLoaded = message.Media != null;
            IsMediaPlaying = message.State == MediaPlayerState.Playing;
            MediaItem = message.Media;
        }

        public double ScreenWidth
        {
            get
            {
                return DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density;
            }
        }

        public bool IsUserLoggedIn
        {
            get
            {
                return _isLoggedIn;
            }
            set
            {
                if (_isLoggedIn != value)
                {
                    _isLoggedIn = value;

                    RaisePropertyChanged(() => IsUserLoggedIn);
                }
            }
        }

        public override void Prepare(object param)
        {
            NavigationParameter = param;
        }

        public override async Task Initialize()
        {
            await base.Initialize();

            IsMediaLoaded = MediaService.CurrentItem != null;
            IsMediaPlaying = MediaService.State == MediaPlayerState.Playing;
            MediaItem = MediaService.CurrentItem;

        }
        public override void ViewAppearing()
        {
            base.ViewAppearing();

        }

        public virtual string PageTitle
        {
            get
            {
                return _pageTitle;
            }
            set
            {
                if (value != _pageTitle)
                {
                    _pageTitle = value;
                    RaisePropertyChanged(() => PageTitle);
                }
            }
        }

        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }
            set
            {
                if (value != _isBusy)
                {
                    _isBusy = value;
                    RaisePropertyChanged(() => IsBusy);
                }
            }
        }

        #region Navigation
        public async Task Navigate<I>() where I : class, IViewModelBase
        {
            var type = Mvx.IoCProvider.Resolve<I>();

            await NavigationService.Navigate(type.GetType());
        }

        public async Task Navigate<I>(object arg) where I : class, IViewModelBase
        {
            var type = Mvx.IoCProvider.Resolve<I>();
            await NavigationService.Navigate<object>(type.GetType(), arg);
        }

        public async Task Navigate (IViewModelBase viewModel, object data)
        {
            await NavigationService.Navigate(viewModel, data);
        }

        protected async Task Close()
        {
            await NavigationService.Close(this);
        }
        #endregion

        public ICommand ShowLoginCommand
        {
            get
            {
                return new MvxAsyncCommand(async () => {
                    await Navigate<ILoginViewModel>();
                });
            }
        }

        public ICommand ShowProfileCommand
        {
            get
            {
                return new MvxAsyncCommand(async () => {
                    await Navigate<IProfileViewModel>();
                });
            }
        }

        public ICommand CloseCommand
        {
            get
            {
                return new MvxAsyncCommand(async () => {
                    await Close();
                });
            }
        }

        #region Media Service
        public bool IsMediaLoaded
        {
            get
            {
                return _isMediaLoaded;
            }
            set
            {
                if (value != _isMediaLoaded)
                {
                    _isMediaLoaded = value;
                    RaisePropertyChanged(() => IsMediaLoaded);
                }
            }
        }

        public bool IsMediaPlaying
        {
            get
            {
                return _isMediaPlaying;
            }
            set
            {
                if (value != _isMediaPlaying)
                {
                    _isMediaPlaying = value;
                    RaisePropertyChanged(() => IsMediaPlaying);
                }
            }
        }

        public MediaAsset MediaItem
        {
            get
            {
                return _mediaAsset;
            }
            set
            {
                if (value != _mediaAsset)
                {
                    _mediaAsset = value;
                    RaisePropertyChanged(() => MediaItem);
                }
            }
        }

        protected async Task ShowPlayer()
        {
            switch(MediaService.CurrentItem.Playback)
            {
                case MediaPlaybackType.VideoHorizontal:                            
                    await Navigate<IHorizontalVideoPlayerViewModel>();
                    break;
                case MediaPlaybackType.VideoVertical:
                    await Navigate<IVerticalVideoPlayerViewModel>();
                    break;
                case MediaPlaybackType.Audio:
                    await Navigate<IAudioPlayerViewModel>();
                    break;
            }
        }

        public ICommand PlayPauseMediaCommand
        {
            get
            {
                return new MvxAsyncCommand<object>(async (object item) => {                    
                    await MediaService.PlayPause();
                });
            }
        }

        public ICommand ShowPlayerCommand
        {
            get
            {
                return new MvxAsyncCommand(async () => {
                    await ShowPlayer();
                });
            }
        }

        public ImageSource PageIconImageSource
        {
            get
            {
                return _pageIconImageSource;
            }
            set
            {
                if (value != _pageIconImageSource)
                {
                    _pageIconImageSource = value;
                    RaisePropertyChanged(() => PageIconImageSource);
                }
            }
        }
        #endregion
    }
}

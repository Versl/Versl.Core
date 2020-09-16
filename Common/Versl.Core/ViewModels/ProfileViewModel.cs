using System.Windows.Input;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.Plugin.Messenger;
using Versl.Services;
using Versl.Models;
using Versl.Services.Auth;
using Versl.Services.Data;
using Versl.Services.Media;
using Versl.Services.Analytics;

namespace Versl.ViewModels
{
    public class ProfileViewModel : ViewModelBase, IProfileViewModel
    {
        private User _currentUser;

        protected IAuthenticationService AuthenticationService;

        public ProfileViewModel(IAuthenticationService authenticationService, IMvxMessenger messenger, IContentDataService contentService, IMvxNavigationService navigationService, IUserInteractionService userInteractionService, IMediaService mediaService, IAnalyticsService analyticsService) : base(messenger, navigationService, userInteractionService, mediaService, analyticsService)
        {
            AuthenticationService = authenticationService;
        }

        public override void Start()
        {
            base.Start();
            CurrentUser = AuthenticationService.CurrentUser;
        }

        public ICommand EditProfileCommand
        {
            get
            {
                return new MvxAsyncCommand(async () => {
                    await Navigate<IEditProfileViewModel>();
                });
            }
        }

        public User CurrentUser
        {
            get
            {
                return _currentUser;
            }
            set
            {
                if (_currentUser != value)
                {
                    _currentUser = value;

                    RaisePropertyChanged(() => CurrentUser);
                }
            }
        }

    }
}

using System;
using System.Windows.Input;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.Plugin.Messenger;
using Versl.Services;
using Versl.Services.Analytics;
using Versl.Services.Auth;
using Versl.Services.Media;

namespace Versl.ViewModels
{
    public class ForgotPasswordViewModel : ViewModelBase, IForgotPasswordViewModel
    {
        private string _username;

        protected IAuthenticationService AuthenticationService;

        public ForgotPasswordViewModel(IMvxMessenger messenger, IMvxNavigationService navigationService, IUserInteractionService userInteractionService, IMediaService mediaService, IAuthenticationService authenticationService, IAnalyticsService analyticsService) : base(messenger, navigationService, userInteractionService, mediaService, analyticsService)
        {
            AuthenticationService = authenticationService;
        }

        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                _username = value;
                RaisePropertyChanged(() => Username);
            }
        }

        public ICommand RecoverPasswordCommand
        {
            get
            {
                return new MvxAsyncCommand(async () => {
                    try
                    {
                        await AuthenticationService.SendPasswordResetEmailAsync(Username);
                        await UserInteractionService.DisplayAlert("Recover Password", "A password reset message has been sent to your email.", "Ok");
                        await Close();
                    }
                    catch(Exception ex)
                    {
                        await UserInteractionService.DisplayAlert("Error", "Account not found.", "Ok");                        
                    }
                });
            }
        }
    }
}

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
    public class LoginViewModel : ViewModelBase, ILoginViewModel
    {
        private string _username;
        private string _password;

        protected IAuthenticationService AuthenticationService;

        public LoginViewModel(IMvxMessenger messenger, IMvxNavigationService navigationService, IUserInteractionService userInteractionService, IMediaService mediaService, IAuthenticationService authenticationService, IAnalyticsService analyticsService) : base(messenger, navigationService, userInteractionService, mediaService, analyticsService)
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

        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                RaisePropertyChanged(() => Password);
            }
        }

        public ICommand LoginCommand
        {
            get
            {
                return new MvxAsyncCommand(async () => {
                    try
                    {
                        await AuthenticationService.LoginAsync(Username, Password);

                        var user = AuthenticationService.CurrentUser;
                        await Close();
                    }
                    catch(Exception ex)
                    {
                        await UserInteractionService.DisplayAlert("Error", "Invalid username and password combination", "Ok");
                        Username = Password = string.Empty;
                    }
                });
            }
        }

        public ICommand ForgotPasswordCommand
        {
            get
            {
                return new MvxAsyncCommand(async () => {
                    await Close();
                    await Navigate<IForgotPasswordViewModel>();
                });
            }
        }

        public ICommand RegisterCommand
        {
            get
            {
                return new MvxAsyncCommand(async () => {
                    await Close();
                    await Navigate<IRegisterViewModel>();
                });
            }
        }
    }
}

using System;
using System.Windows.Input;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.Plugin.Messenger;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Versl.Services;
using Versl.Services.Storage;
using Versl.Models;
using Versl.Services.Auth;
using Versl.Services.Media;
using Xamarin.Forms;
using Versl.Services.Analytics;

namespace Versl.ViewModels
{
    public class RegisterViewModel : ViewModelBase, IRegisterViewModel
    {
        private string _username;
        private string _password;
        private string _displayName;
        private string _photoUrl;
        private ImageSource _imageSource = ImageSource.FromFile("brandlogo.png");

        protected IAuthenticationService AuthenticationService;
        protected IStorageService StorageService;

        public RegisterViewModel(IStorageService storageService, IMvxMessenger messenger, IMvxNavigationService navigationService, IUserInteractionService userInteractionService, IMediaService mediaService, IAuthenticationService authenticationService, IAnalyticsService analyticsService) : base(messenger, navigationService, userInteractionService, mediaService, analyticsService)
        {
            AuthenticationService = authenticationService;
            StorageService = storageService;
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

        public string DisplayName
        {
            get
            {
                return _displayName;
            }
            set
            {
                _displayName = value;
                RaisePropertyChanged(() => DisplayName);
            }
        }

        public ImageSource ImageSource
        {
            get
            {
                return _imageSource;
            }
            set
            {
                if (value != _imageSource)
                {
                    _imageSource = value;
                    RaisePropertyChanged(() => ImageSource);
                }
            }
        }

        private MediaFile SelectedImage { get; set; }

        public ICommand SelectImageCommand
        {
            get
            {
                return new MvxAsyncCommand(async () => {
                    SelectedImage = await CrossMedia.Current.PickPhotoAsync();

                    if (SelectedImage == null)
                        return;

                    ImageSource = ImageSource.FromStream(() =>
                    {
                        var stream = SelectedImage.GetStream();
                        return stream;
                    });
                });
            }
        }

        public ICommand RegisterCommand
        {
            get
            {
                return new MvxAsyncCommand(async () => {
                    try
                    {
                        var request = new CreateUserRequest();

                        request.Email = Username;
                        request.DisplayName = DisplayName;
                        request.Password = Password;
                        
                        if (SelectedImage != null)
                        {
                            request.Photo = SelectedImage.GetStream();
                        }

                        await AuthenticationService.CreateUserAsync(request);
                        
                        await Close();                        
                    }
                    catch (Exception ex)
                    {
                        await UserInteractionService.DisplayAlert("Error", "Invalid username and password combination", "Ok");
                        Username = Password = string.Empty;
                    }
                });
            }
        }
    }
}

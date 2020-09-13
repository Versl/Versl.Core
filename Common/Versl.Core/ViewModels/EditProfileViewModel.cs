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
using Versl.Services.Data;
using Versl.Services.Media;
using Xamarin.Forms;
using Versl.Services.Analytics;

namespace Versl.ViewModels
{    
    public class EditProfileViewModel : ProfileViewModel, IEditProfileViewModel
    {
        private ImageSource _imageSource;
        private string _displayName;

        protected IStorageService StorageService;

        public EditProfileViewModel(IStorageService storageService, IAuthenticationService authenticationService, IMvxMessenger messenger, IContentDataService contentService, IMvxNavigationService navigationService, IUserInteractionService userInteractionService, IMediaService mediaService, IAnalyticsService analyticsService) : base(authenticationService, messenger, contentService, navigationService, userInteractionService, mediaService, analyticsService)
        {
            StorageService = storageService;
        }

        public override void ViewAppearing()
        {
            base.ViewAppearing();

            DisplayName = CurrentUser.DisplayName;
            ImageSource = CurrentUser.PhotoUrl;
        }

        protected MediaFile SelectedImage { get; set; }

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

        public string DisplayName
        {
            get
            {
                return _displayName;
            }
            set
            {
                if (value != _displayName)
                {
                    _displayName = value;
                    RaisePropertyChanged(() => DisplayName);
                }
            }
        }

        public ICommand SaveProfileCommand
        {
            get
            {
                return new MvxAsyncCommand(async () => {

                    var request = new UpdateUserRequest();

                    request.Email = CurrentUser.Email;
                    request.DisplayName = DisplayName;
                    if (SelectedImage != null)
                    {
                        request.Photo = SelectedImage.GetStream();
                    }

                    await AuthenticationService.UpdateProfileAsync(request);
                    await Close();
                });
            }
        }

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
    }
}

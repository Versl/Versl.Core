using System;
using System.Threading.Tasks;
using AutoMapper;
using MvvmCross;
using MvvmCross.Plugin.Messenger;
using Versl.Enums;
using Versl.Models;
using Versl.Services.Auth;
using Versl.Services.Data;
using Versl.Services.Storage;
using Versl.Utility;
using FBAuth = Firebase.Auth;

namespace Versl.Firebase.Auth
{
    public class AuthenticationService : IAuthenticationService
    {
        FBAuth.FirebaseAuthProvider Provider;
        IMvxMessenger _messenger;
        IStorageService StorageService;
        IUserProfileDataService _userProfileDataService;
        IMapper _mapper;

        public AuthenticationService(IMapper mapper, IUserProfileDataService userProfileDataService, IStorageService storageService, IMvxMessenger messenger) 
        {
            var config = Mvx.IoCProvider.Resolve<IFirebaseConfig>();

            _mapper = mapper;
            Provider = new FBAuth.FirebaseAuthProvider(new FBAuth.FirebaseConfig(config.APIKey));
            StorageService = storageService;
            _userProfileDataService = userProfileDataService;

            _messenger = messenger;
        }

        public async Task CreateUserAsync(CreateUserRequest request)
        {
            var authLink = await Provider.CreateUserWithEmailAndPasswordAsync(request.Email, request.Password);

            var userProfile = _mapper.Map<UserProfile>(request);
            userProfile.Id = authLink.User.LocalId;
            userProfile.Role = UserRole.Subscriber;

            if (request.Photo != null)
            {
                userProfile.PhotoUrl = await StorageService.PutAsync(StorageFolder.Profiles, authLink.User.LocalId, request.Photo);                
            }

            await _userProfileDataService.UpdateAsync(userProfile);

            CurrentUser = await CreateUser(authLink.User.LocalId, authLink.FirebaseToken, authLink.RefreshToken); ;

            _messenger.Publish(new AuthenticationMessage(this, CurrentUser));
        }

        public async Task LoginAsync(string email, string password)
        {
            var authLink = await Provider.SignInWithEmailAndPasswordAsync(email, password);

            CurrentUser = await CreateUser(authLink.User.LocalId, authLink.FirebaseToken, authLink.RefreshToken);
            _messenger.Publish(new AuthenticationMessage(this, CurrentUser));
        }

        public async Task RefreshLoginAsync (string token)
        {
            var authLink = await Provider.RefreshAuthAsync(new FBAuth.FirebaseAuth() { RefreshToken = token });

            if (string.IsNullOrEmpty(authLink.FirebaseToken))
            {
                CurrentUser = null;
                return;
            }
            var authUser = await Provider.GetUserAsync(authLink.FirebaseToken);

            CurrentUser = await CreateUser(authUser.LocalId, authLink.FirebaseToken, authLink.RefreshToken);
            _messenger.Publish(new AuthenticationMessage(this, CurrentUser));
        }

        public async Task SendPasswordResetEmailAsync(string email)
        {
            await Provider.SendPasswordResetEmailAsync(email);
        }

        public async Task UpdateProfileAsync(UpdateUserRequest request)
        {
            var userProfile = _mapper.Map<UserProfile>(request);

            if (request.Photo != null)
            {
                userProfile.PhotoUrl = await StorageService.PutAsync(StorageFolder.Profiles, CurrentUser.Id, request.Photo);
            }

            userProfile.Id = CurrentUser.Id;

            await _userProfileDataService.UpdateAsync(userProfile);
            var user = _mapper.Map<User>(userProfile);

            user.AuthToken = CurrentUser.AuthToken;

            CurrentUser = user;

            _messenger.Publish(new AuthenticationMessage(this, CurrentUser));
        }

        public async Task LogoutAsync()
        {
            CurrentUser = null;
            Settings.RefreshToken = null;

            _messenger.Publish(new AuthenticationMessage(this, CurrentUser));
        }

        public User CurrentUser { get; private set; }

        private async Task<User> CreateUser(string id, string authToken, string refreshToken)
        {
            var userProfile = await _userProfileDataService.GetAsync(id);
            var user = _mapper.Map<User>(userProfile);

            user.AuthToken = authToken;
            user.RefreshToken = refreshToken;
            user.Id = id;

            Settings.RefreshToken = user.RefreshToken;

            return user;
        }
    }
}

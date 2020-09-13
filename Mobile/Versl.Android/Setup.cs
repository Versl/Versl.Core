﻿using System;
using System.Collections.Generic;
using AutoMapper;
using MvvmCross;
using MvvmCross.Forms.Platforms.Android.Core;
using MvvmCross.Forms.Presenters;
using MvvmCross.Logging;
using MvvmCross.ViewModels;
using MvvmCross.Views;
using Versl.Droid.Analytics;
using Versl.Core;
using Versl.Firebase;
using Versl.Firebase.Auth;
using Versl.Firebase.Data;
using Versl.Firebase.Data.DTO;
using Versl.Firebase.Data.Services;
using Versl.Firebase.Push;
using Versl.Firebase.Storage;
using Versl.Models;
using Versl.Services;
using Versl.Services.Analytics;
using Versl.Services.Auth;
using Versl.Services.Data;
using Versl.Services.Media;
using Versl.Services.Push;
using Versl.Services.Storage;
using Versl.Services.Theme;
using Versl.ViewModels;
using Versl.ViewModels.Admin;
using Xamarin.Forms;

namespace Versl.Droid
{
    public class Setup : MvxFormsAndroidSetup<CoreApp, VerslApp>, IFirebaseConfigurationReader
    {
        public override MvxLogProviderType GetDefaultLogProviderType()
        {            
            return MvvmCross.Logging.MvxLogProviderType.Console;
        }

        protected override IMvxFormsPagePresenter CreateFormsPagePresenter(IMvxFormsViewPresenter viewPresenter)
        {
            // Workaround/fix for: https://github.com/MvvmCross/MvvmCross/issues/2502
            var formsPagePresenter = new AppMvxFormsPagePresenter(viewPresenter);
            Mvx.IoCProvider.RegisterSingleton<IMvxFormsPagePresenter>(formsPagePresenter);
            return formsPagePresenter;
        }

        public IFirebaseConfig GetFirebaseConfiguration()
        {
            var config = new FirebaseConfig();

            config.DatabaseURL = ApplicationContext.Resources.GetString(Resource.String.firebase_database_url);
            config.APIKey = ApplicationContext.Resources.GetString(Resource.String.google_api_key);
            config.AuthDomain = ApplicationContext.Resources.GetString(Resource.String.google_storage_bucket);
            config.ProjectID = ApplicationContext.Resources.GetString(Resource.String.project_id);
            config.StorageBucket = ApplicationContext.Resources.GetString(Resource.String.google_storage_bucket);
            config.MessagingSenderID = ApplicationContext.Resources.GetString(Resource.String.gcm_defaultSenderId);
            config.AppID = ApplicationContext.Resources.GetString(Resource.String.google_app_id);

            return config;
        }

        protected override IMvxApplication CreateApp()
        {
            //Firebase
            Mvx.IoCProvider.RegisterSingleton<IFirebaseConfig>(GetFirebaseConfiguration());

            #region Automapper Configuration
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<User, UserProfile>().ReverseMap();
                cfg.CreateMap<UserProfile, UserProfileDTO>().ReverseMap();
                cfg.CreateMap<UserProfile, CreateUserRequest>().ReverseMap();
                cfg.CreateMap<UserProfile, UpdateUserRequest>().ReverseMap();
            });

            Mvx.IoCProvider.RegisterSingleton<IMapper>(config.CreateMapper());
            #endregion

            #region Register Services            
            //Services
            Mvx.IoCProvider.RegisterSingleton<IUserInteractionService>(() => UserInteractionService.DefaultInstance);
            Mvx.IoCProvider.RegisterSingleton<IMediaService>(() => Mvx.IoCProvider.IoCConstruct<MediaService>());
            Mvx.IoCProvider.RegisterSingleton<IAuthenticationService>(() => Mvx.IoCProvider.IoCConstruct<AuthenticationService>());
            Mvx.IoCProvider.RegisterSingleton<IStorageService>(() => Mvx.IoCProvider.IoCConstruct<StorageService>());
            Mvx.IoCProvider.RegisterSingleton<IAnalyticsService>(() => Mvx.IoCProvider.IoCConstruct<AnalyticsService>());
            Mvx.IoCProvider.RegisterSingleton<IPushNotificationService>(() => Mvx.IoCProvider.IoCConstruct<PushNotificationService>());
            Mvx.IoCProvider.RegisterSingleton<IThemeService>(() => Mvx.IoCProvider.IoCConstruct<ThemeService>());

            //Data Services
            Mvx.IoCProvider.RegisterSingleton<ILayoutConfigurationDatabaseService>(() => Mvx.IoCProvider.IoCConstruct<LayoutConfigurationDatabaseService>());
            Mvx.IoCProvider.RegisterSingleton<ILayoutConfigurationDataService>(() => Mvx.IoCProvider.IoCConstruct<LayoutConfigurationDataService>());

            Mvx.IoCProvider.RegisterSingleton<IUserProfileDatabaseService>(() => Mvx.IoCProvider.IoCConstruct<UserProfileDatabaseService>());
            Mvx.IoCProvider.RegisterSingleton<IUserProfileDataService>(() => Mvx.IoCProvider.IoCConstruct<UserProfileDataService>());

            Mvx.IoCProvider.RegisterSingleton<IContentDatabaseService>(() => Mvx.IoCProvider.IoCConstruct<ContentDatabaseService>());
            Mvx.IoCProvider.RegisterSingleton<IContentDataService>(() => Mvx.IoCProvider.IoCConstruct<ContentDataService>());

            Mvx.IoCProvider.RegisterSingleton<ICategoryDatabaseService>(() => Mvx.IoCProvider.IoCConstruct<CategoryDatabaseService>());
            Mvx.IoCProvider.RegisterSingleton<ICategoryDataService>(() => Mvx.IoCProvider.IoCConstruct<CategoryDataService>());
            #endregion

            #region Register Configuration
            var layoutService = Mvx.IoCProvider.Resolve<ILayoutConfigurationDataService>();
            var layout = layoutService.GetLayoutConfigurationAsync().ConfigureAwait(false).GetAwaiter().GetResult();
            Mvx.IoCProvider.RegisterSingleton<ILayoutConfiguration>(layout);
            #endregion

            #region Register ViewModel Types
            //ViewModels
            Mvx.IoCProvider.RegisterType<IMainViewModel, RootViewModel>();
            Mvx.IoCProvider.RegisterType<IMainMasterDetailMasterViewModel, MainMasterDetailMasterViewModel>();
            Mvx.IoCProvider.RegisterType<IMainMasterDetailDetailViewModel, MainMasterDetailDetailViewModel>();
            Mvx.IoCProvider.RegisterType<IContentDetailViewModel, ContentDetailViewModel>();
            Mvx.IoCProvider.RegisterType<IContentListViewModel, ContentListViewModel>();
            Mvx.IoCProvider.RegisterType<IContentDetailViewModel, ContentDetailViewModel>();
            Mvx.IoCProvider.RegisterType<ITabbedContentDetailViewModel, TabbedContentDetailViewModel>();
            Mvx.IoCProvider.RegisterType<IWebViewViewModel, WebViewViewModel>();
            Mvx.IoCProvider.RegisterType<ILoginViewModel, LoginViewModel>();
            Mvx.IoCProvider.RegisterType<IRegisterViewModel, RegisterViewModel>();
            Mvx.IoCProvider.RegisterType<IForgotPasswordViewModel, ForgotPasswordViewModel>();
            Mvx.IoCProvider.RegisterType<IProfileViewModel, ProfileViewModel>();
            Mvx.IoCProvider.RegisterType<IEditProfileViewModel, EditProfileViewModel>();
            Mvx.IoCProvider.RegisterType<IAudioPlayerViewModel, AudioPlayerViewModel>();
            Mvx.IoCProvider.RegisterType<IHorizontalVideoPlayerViewModel, HorizontalVideoPlayerViewModel>();
            Mvx.IoCProvider.RegisterType<IVerticalVideoPlayerViewModel, VerticalVideoPlayerViewModel>();

            //Admin
            Mvx.IoCProvider.RegisterType<IAdminCategoryListViewModel, AdminCategoryListViewModel>();
            Mvx.IoCProvider.RegisterType<IAdminContentListViewModel, AdminContentListViewModel>();
            Mvx.IoCProvider.RegisterType<IAdminAddContentViewModel, AdminAddContentViewModel>();
            Mvx.IoCProvider.RegisterType<IAdminEditContentViewModel, AdminEditContentViewModel>();
            Mvx.IoCProvider.RegisterType<IAdminPushNotificationsViewModel, AdminPushNotificationsViewModel>();
            Mvx.IoCProvider.RegisterType<IAdminAdminUserListViewModel, AdminAdminUserListViewModel>();
            #endregion

            return new Versl.CoreApp();
        }

        protected override IMvxViewsContainer InitializeViewLookup(IDictionary<Type, Type> viewModelViewLookup)
        {
            //viewModelViewLookup.Add(typeof(RootViewModel), typeof(RootPage));

            return base.InitializeViewLookup(viewModelViewLookup);
        }

        protected override Application CreateFormsApplication()
        {
            return new VerslApp();
        }
    }
}

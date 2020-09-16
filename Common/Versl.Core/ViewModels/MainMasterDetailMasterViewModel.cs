using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.Plugin.Messenger;
using Versl.Enums;
using Versl.Models;
using Versl.Services;
using Versl.Services.Analytics;
using Versl.Services.Auth;
using Versl.Services.Media;
using Versl.Utility;
using Xamarin.Essentials;
using System;
using System.Threading.Tasks;

namespace Versl.ViewModels
{
    public class MainMasterDetailMasterViewModel : ViewModelBase, IMainMasterDetailMasterViewModel
    {
        private object _selectedItem;        
        private ObservableCollection<MenuLink> _menuLinks;
        private readonly MvxSubscriptionToken _authenticationToken;
        private IMvxNavigationService _navigationService;
        private ILayoutConfiguration _layout;

        protected IAuthenticationService AuthenticationService;

        public MainMasterDetailMasterViewModel(IMvxMessenger messenger, IAuthenticationService authenticationService, ILayoutConfiguration appConfiguration, IMvxNavigationService navigationService, IUserInteractionService userInteractionService, IMediaService mediaService, IAnalyticsService analyticsService) : base(messenger, navigationService, userInteractionService, mediaService, analyticsService)
        {
            _layout = appConfiguration;
            AuthenticationService = authenticationService;

            _authenticationToken = messenger.Subscribe<AuthenticationMessage>(HandleAuthentication);
            _navigationService = navigationService;
        }

        private void HandleAuthentication(AuthenticationMessage message)
        {
            MenuLinks = new ObservableCollection<MenuLink>(GetMenu());
        }

        public override Task Initialize()
        {
            MenuLinks = new ObservableCollection<MenuLink>(GetMenu());
            RaisePropertyChanged(() => MenuLinks);

            return base.Initialize();
        }

        public string AppInformation
        {
            get
            {
                return "Version " + AppInfo.VersionString + " (Build " + AppInfo.BuildString + ")";
            }
        }

        public ILayoutConfiguration Layout
        {
            get
            {
                return _layout;
            }
            set
            {
                if (_layout != value)
                {
                    _layout = value;

                    RaisePropertyChanged(() => Layout);
                }
            }
        }

        public ObservableCollection<MenuLink> MenuLinks
        {
            get
            {
                return _menuLinks;
            }
            set
            {
                if (_menuLinks != value)
                {
                    _menuLinks = value;

                    RaisePropertyChanged(() => MenuLinks);
                }
            }
        }

        protected virtual List<MenuLink> GetMenu()
        {
            var menu = new List<MenuLink>();

            var generalMenu = new List<MenuLink>();
            foreach (MenuLink menuLink in _layout.Menu.Links)
            {
                var link = new MenuLink(_navigationService, AuthenticationService);
                link.Title = menuLink.Title;
                link.Type = menuLink.Type;
                link.Url = menuLink.Url;
                link.Color = ColorResolver.Resolve(ApplicationColor.OnBackground).ToHex();
                link.Icon = new Icon();
                link.Icon.Glyph = menuLink.Icon.Glyph;
                link.Icon.Family = FontAwesomeFontFamilyResolver.Resolve(menuLink.Icon.Family);
                generalMenu.Add(link);
            }

            menu.AddRange(generalMenu);
            menu.AddRange(GetUserMenu());
            menu.AddRange(GetAdminMenu());

            return menu;
        }

        protected virtual List<MenuLink> GetUserMenu()
        {
            var menu = new List<MenuLink>();

            if (AuthenticationService.CurrentUser == null)
            {
                menu.Add(new MenuLink(_navigationService, AuthenticationService)
                {
                    Title = "Login",
                    Icon = new Icon() { Glyph = FontAwesomeIcons.SignInAlt, Family = FontAwesomeFontFamilyResolver.Resolve(FontAwesomeFontFamily.Solid) },
                    Type = MenuLinkType.Page,
                    Url = "ILoginViewModel"
                });
            }
            else
            {
                menu.Add(new MenuLink(_navigationService, AuthenticationService)
                {
                    Title = "Profile",
                    Icon = new Icon() { Glyph = FontAwesomeIcons.User, Family = FontAwesomeFontFamilyResolver.Resolve(FontAwesomeFontFamily.Solid) },
                    Type = MenuLinkType.Page,
                    Url = "IProfileViewModel"
                });

                menu.Add(new MenuLink(_navigationService, AuthenticationService)
                {
                    Title = "Logout",
                    Icon = new Icon() { Glyph = FontAwesomeIcons.SignOutAlt, Family = FontAwesomeFontFamilyResolver.Resolve(FontAwesomeFontFamily.Solid) },
                    Type = MenuLinkType.Page,
                    Url = "ILogoutViewModel"
                });
            }

            return menu;
        }

        protected virtual List<MenuLink> GetAdminMenu()
        {
            var menu = new List<MenuLink>();

            if (AuthenticationService.CurrentUser == null || AuthenticationService.CurrentUser.Role != UserRole.Admin)
            {
                return menu;
            }

            menu.Add(new MenuLink(_navigationService, AuthenticationService)
            {
                Title = "Folders",
                Icon = new Icon() { Glyph = FontAwesomeIcons.Folder, Family = FontAwesomeFontFamilyResolver.Resolve(FontAwesomeFontFamily.Solid) },
                Color = ColorResolver.Resolve(ApplicationColor.VerslPurple).ToString(),
                Type = MenuLinkType.Page,
                Url = "IAdminCategoryListViewModel"
            }); ; ;

            menu.Add(new MenuLink(_navigationService, AuthenticationService)
            {
                Title = "Posts",
                Icon = new Icon() { Glyph = FontAwesomeIcons.File, Family = FontAwesomeFontFamilyResolver.Resolve(FontAwesomeFontFamily.Solid) },
                Color = ColorResolver.Resolve(ApplicationColor.VerslPurple).ToString(),
                Type = MenuLinkType.Page,
                Url = "IAdminContentListViewModel"
            });

            menu.Add(new MenuLink(_navigationService, AuthenticationService)
            {
                Title = "Push Notifications",
                Icon = new Icon() { Glyph = FontAwesomeIcons.Bell, Family = FontAwesomeFontFamilyResolver.Resolve(FontAwesomeFontFamily.Solid) },
                Color = ColorResolver.Resolve(ApplicationColor.VerslPurple).ToString(),
                Type = MenuLinkType.Page,
                Url = "IAdminPushNotificationsViewModel"
            });

            menu.Add(new MenuLink(_navigationService, AuthenticationService)
            {
                Title = "Admins",
                Icon = new Icon() { Glyph = FontAwesomeIcons.Users, Family = FontAwesomeFontFamilyResolver.Resolve(FontAwesomeFontFamily.Solid) },
                Color = ColorResolver.Resolve(ApplicationColor.VerslPurple).ToString(),
                Type = MenuLinkType.Page,
                Url = "IAdminAdminUserListViewModel"
            });

            return menu;
        }

        public virtual object SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                if (_selectedItem != value)
                {
                    _selectedItem = value;

                    RaisePropertyChanged(() => SelectedItem);
                    if (value != null)
                    {
                        var link = value as MenuLink;

                        if (link.Command != null && link.Command.CanExecute(value))
                        {
                            link.Command.Execute(value);
                        }
                    }
                    SelectedItem = null;
                }
            }
        }

        public ICommand VendorInfoCommand
        {
            get
            {
                return new MvxAsyncCommand(async () => {
                    await Browser.OpenAsync("https://myversl.com", new BrowserLaunchOptions
                    {
                        LaunchMode = BrowserLaunchMode.SystemPreferred,
                        TitleMode = BrowserTitleMode.Show
                    });
                });
            }
        }
    }
}

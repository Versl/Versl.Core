using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Newtonsoft.Json;
using Versl.Enums;
using Versl.Services.Auth;
using Xamarin.Essentials;

namespace Versl.Models
{
    public class MenuLink : LinkBase
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly IAuthenticationService _authenticationService;

        public MenuLink(IMvxNavigationService navigationService, IAuthenticationService authenticationService)
        {
            _navigationService = navigationService;
            _authenticationService = authenticationService;
        }

        [JsonProperty("type")]
        public MenuLinkType Type
        {
            get; set;
        }

        [JsonIgnore]
        public ICommand Command
        {
            get
            {
                switch (Type)
                {
                    case MenuLinkType.Email:
                        return new MvxAsyncCommand(async () => {
                            var recipients = new List<string>(new string[] { Url });

                            var message = new EmailMessage
                            {
                                To = recipients
                            };
                            await Email.ComposeAsync(message);
                        });
                    case MenuLinkType.Page:
                        return new MvxAsyncCommand(async () => {

                            if (Url.ToLower() == "ilogoutviewmodel")
                            {
                                await _authenticationService.LogoutAsync();
                                return;
                            }

                            var type = AppDomain.CurrentDomain
                                .GetAssemblies()
                                .SelectMany(x => x.GetTypes())
                                .FirstOrDefault(t => t.Name == Url);

                            var vm = Mvx.IoCProvider.Resolve(type) as IMvxViewModel;
                            
                            await _navigationService.Navigate(vm);
                        });
                    case MenuLinkType.Web:
                    default:
                        return new MvxAsyncCommand(async () => {
                            await Browser.OpenAsync(Url, new BrowserLaunchOptions
                            {
                                LaunchMode = BrowserLaunchMode.SystemPreferred,
                                TitleMode = BrowserTitleMode.Show
                            });
                        });
                }
            }
        }
    }
}

using System;
using System.Reflection;
using System.Threading.Tasks;
using MvvmCross;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Versl.Models;
using Versl.Services.Auth;
using Versl.Services.Theme;
using Versl.ViewModels;
using Xamarin.Forms;

namespace Versl
{
    public class AppStart : IMvxAppStart
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly IAuthenticationService _authenticationService;
        private readonly IThemeService _themeService;

        public AppStart(IThemeService themeService, IAuthenticationService authenticationService, IMvxNavigationService mvxNavigationService)
        {
            _navigationService = mvxNavigationService;
            _authenticationService = authenticationService;
            _themeService = themeService;
        }

        public bool IsStarted => false;

        public void ResetStart()
        {
            throw new NotImplementedException();
        }

        public async void Start(object hint = null)
        {
            var master = Mvx.IoCProvider.Resolve<IMainViewModel>();
            await _navigationService.Navigate((IMvxViewModel)master);
        }

        public async Task StartAsync(object hint = null)
        {            
            var master = Mvx.IoCProvider.Resolve<IMainViewModel>();
            await _navigationService.Navigate((IMvxViewModel)master);
        }
    }
}

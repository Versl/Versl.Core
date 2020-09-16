using System.Threading.Tasks;
using MvvmCross;
using MvvmCross.Navigation;
using MvvmCross.Plugin.Messenger;
using Versl.Services;
using Versl.Models;
using Versl.Services.Media;
using Versl.Services.Analytics;
using System;
using System.Linq;
using MvvmCross.ViewModels;
using Xamarin.Forms;
using Versl.Utility;
using Tab = Versl.Models.Tab;

namespace Versl.ViewModels
{
    public class MainMasterDetailDetailViewModel : ViewModelBase, IMainMasterDetailDetailViewModel
    {
        private ILayoutConfiguration _layoutConfiguration;
        private bool _hasAppeared = false;

        public MainMasterDetailDetailViewModel(IMvxMessenger messenger, ILayoutConfiguration appConfiguration, IMvxNavigationService navigationService, IUserInteractionService userInteractionService, IMediaService mediaService, IAnalyticsService analyticsService) : base(messenger, navigationService, userInteractionService, mediaService, analyticsService)
        {
            _layoutConfiguration = appConfiguration;
        }

        public override string PageTitle { get => _layoutConfiguration.Title; }

        public override async void ViewAppearing()
        {
            base.ViewAppearing();
            await ShowTabViewModels();
        }

        protected virtual async Task ShowTabViewModels()
        {
            if (!_hasAppeared)
            {
                foreach (Tab tab in this._layoutConfiguration.Tabs)
                {
                    var type = AppDomain.CurrentDomain
                                    .GetAssemblies()
                                    .SelectMany(x => x.GetTypes())
                                    .FirstOrDefault(t => t.Name == tab.Type);

                    var vm = Mvx.IoCProvider.Resolve(type) as IViewModelBase;
                    
                    var fontImageSource = new FontImageSource();

                    fontImageSource.FontFamily = FontAwesomeFontFamilyResolver.Resolve(tab.Icon.Family);
                    fontImageSource.Glyph = tab.Icon.Glyph;
                    fontImageSource.Size = 16;

                    vm.PageIconImageSource = fontImageSource;
                    vm.PageTitle = tab.Title;

                    await Navigate(vm, tab.Data);
                }

                _hasAppeared = true;
            }
        }
    }
}

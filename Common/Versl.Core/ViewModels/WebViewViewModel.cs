using System.Threading.Tasks;
using System.Windows.Input;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.Plugin.Messenger;
using Versl.Services;
using Versl.Services.Analytics;
using Versl.Services.Media;
using Xamarin.Forms;

namespace Versl.ViewModels
{
    public class WebViewViewModel: ViewModelBase, IWebViewViewModel
    {
        private UrlWebViewSource _source;

        public WebViewViewModel(IMvxMessenger messenger, IMvxNavigationService navigationService, IUserInteractionService userInteractionService, IMediaService mediaService, IAnalyticsService analyticsService) : base(messenger, navigationService, userInteractionService, mediaService, analyticsService)
        {
        }

        public override Task Initialize()
        {
            RefreshWebView();

            return base.Initialize();
        }

        private void RefreshWebView()
        {
            var source = new UrlWebViewSource() { Url = NavigationParameter.ToString() };

            Source = source;
        }

        public UrlWebViewSource Source
        {
            get
            {
                return _source;
            }
            private set
            {
                if (_source != value)
                {

                    _source = value;
                    RaisePropertyChanged(() => Source);
                }
            }
        }

        public ICommand RefreshCommand
        {
            get
            {
                return new MvxAsyncCommand(async () => {
                    RefreshWebView();
                });
            }
        }
    }
}

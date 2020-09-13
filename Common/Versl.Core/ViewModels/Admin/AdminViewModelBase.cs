using MvvmCross.Navigation;
using MvvmCross.Plugin.Messenger;
using Versl.Services;
using Versl.Services.Analytics;
using Versl.Services.Media;

namespace Versl.ViewModels.Admin
{
    public abstract class AdminViewModelBase : ViewModelBase
    {
        public AdminViewModelBase(IMvxMessenger messenger, IMvxNavigationService navigationService, IUserInteractionService userInteractionService, IMediaService mediaService, IAnalyticsService analyticsService) : base(messenger, navigationService, userInteractionService, mediaService, analyticsService)
        {
        }
    }
}

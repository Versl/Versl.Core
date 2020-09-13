using System.Windows.Input;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.Plugin.Messenger;
using Versl.Models;
using Versl.Services;
using Versl.Services.Analytics;
using Versl.Services.Data;
using Versl.Services.Media;

namespace Versl.ViewModels.Admin
{    
    public class AdminContentListViewModel : AdminListViewModelBase<ContentItem>, IAdminContentListViewModel
    {
        public AdminContentListViewModel(IMvxMessenger messenger, IContentDataService contentService, ICategoryDataService categoryDataService, IMvxNavigationService navigationService, IUserInteractionService userInteractionService, IMediaService mediaService, IAnalyticsService analyticsService) : base(messenger, navigationService, userInteractionService, mediaService, analyticsService)
        {
            ListService = contentService;
        }

        public override string PageTitle { get => "Posts"; }

        public override ICommand ItemSelectedCommand
        {
            get
            {
                return new MvxAsyncCommand<ContentItem>(async (ContentItem arg) => {

                    var item = (DataModelBase)arg;

                    await Navigate<IAdminEditContentViewModel>(arg.Id);
                });
            }
        }

        public ICommand AddCommand
        {
            get
            {
                return new MvxAsyncCommand<ContentItem>(async (ContentItem arg) => {
                    await Navigate<IAdminAddContentViewModel>();
                });
            }
        }
    }
}
